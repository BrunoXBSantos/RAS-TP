using API.DTOs;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers;

public class MatchController : BaseApiController
{
    private readonly IEventRepository _eventRepository;
    private readonly IBetRepository _betRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IServiceProvider _provider;
    private readonly IObservables _observables;

    public MatchController(IEventRepository eventRepository,
                            IBetRepository betRepository,
                            IAppUserRepository appUserRepository,
                            IServiceProvider provider, 
                            IObservables observables)
    {
        _eventRepository = eventRepository;
        _provider = provider;
        _observables = observables;
        _betRepository = betRepository;
        _appUserRepository = appUserRepository;
    }

    [HttpPost("startmatch")]
    public async Task<ActionResult<MatchDto>> StartMatch(MatchDto startMatchDto)
    {   
        var eventDb = await _eventRepository.GetIdEventByParams(startMatchDto);
        // se evento null, é porque não existe
        if(eventDb == null){
            return NotFound("Event Not Found");
        }

        if(eventDb.eventState.Description.Equals("Finished")){
            return BadRequest("Event is over");
        }

        var description = startMatchDto.Team1 + 
                            " - " + startMatchDto.Team2 + 
                            " Started";

        SendNotification(description, eventDb);
        Console.WriteLine("" + eventDb.Id + eventDb.sport.Description);
        return Ok(startMatchDto);

    }


    [HttpPost("halftime")]
    public async Task<ActionResult<MatchDto>> Halftime(MatchDto halftime)
    {   
        var eventDb = await _eventRepository.GetIdEventByParams(halftime);
        // se evento null, é porque não existe
        if(eventDb == null){
            return NotFound("Event Not Found");
        }

        if(eventDb.eventState.Description.Equals("Finished")){
            return BadRequest("Event is over");
        }

        var description = halftime.Team1 + 
                            " - " + halftime.Team2 + 
                            " HalfTime";

        SendNotification(description, eventDb);
        Console.WriteLine("" + eventDb.Id + eventDb.sport.Description);
        return Ok(halftime);
    }

    [HttpPost("endmatch")]
    public async Task<ActionResult<MatchDto>> EnfMatch(EndMatchDto endMatch)
    {   
        var eventDb = await _eventRepository.GetIdEventByParams(endMatch);
        // se evento null, é porque não existe
        if(eventDb == null){
            return NotFound("Event Not Found");
        }

        if(eventDb.eventState.Description.Equals("Finished")){
            return BadRequest("Event is over");
        }

        var description = endMatch.Team1 +
                            " " + endMatch.Team1Goals + 
                            " - " + endMatch.Team2Goals +
                            endMatch.Team2 + 
                            " END MATCH";

        SendNotification(description, eventDb);
        if (!await calculateEarnings(eventDb, endMatch)){
            return BadRequest("Error to calculate Earnings");
        }
        Console.WriteLine("" + eventDb.Id + eventDb.sport.Description);
        return Ok(endMatch);
    }

    [HttpPost("goalscored")]
    public async Task<ActionResult> GoalScored(GoalScoredDto goalScoredDto)
    {   
        MatchDto matchDto = new MatchDto();
        matchDto.Team1 = goalScoredDto.Team1;
        matchDto.Team2 = goalScoredDto.Team2;
        matchDto.Sport = goalScoredDto.Sport;
        var eventDb = await _eventRepository.GetIdEventByParams(matchDto);
        // se evento null, é porque não existe
        if(eventDb == null){
            return NotFound("Event Not Found");
        }

        if(eventDb.eventState.Description.Equals("Finished")){
            return BadRequest("Event is over");
        }

        var description = goalScoredDto.Team1 + 
                            " - " + goalScoredDto.Team2 + 
                            " Goal " +
                            " " + goalScoredDto.Goal;

        if(goalScoredDto.Played != null)
            description += " Played: " + goalScoredDto.Played;
        

        SendNotification(description, eventDb);
        Console.WriteLine("" + eventDb.Id + eventDb.sport.Description);
        return Ok();
    }


    [NonAction]
    public async Task<bool> calculateEarnings(EventDB eventDB, EndMatchDto endMatch)
    {
        // vou buscar o eventObservable do Evento 
        EventObservable eventObservable = _observables.GetEventObservableByIdEvent(eventDB.Id);

        // a cada observer de eventObservable
        foreach(BetObserver betObserver in eventObservable.observers){
            var bet = await _betRepository.GetBetByIdAsync(betObserver.betId);
            // coloca state da bet  = Finished
            bet.betStateId = 2;
            _betRepository.UpdateBet(bet);
            if (!await _betRepository.SaveAllAsync())
            {
                return false;
            }
            //se ganhou a aposta
            if( endMatch.Team1Goals>endMatch.Team2Goals && bet.Result.Equals("1") || 
                endMatch.Team1Goals<endMatch.Team2Goals && bet.Result.Equals("2") ||
                endMatch.Team1Goals==endMatch.Team2Goals && bet.Result.ToUpper().Equals("X")){
                    float multiplied = 0;
                    if(endMatch.Team1Goals>endMatch.Team2Goals)
                        multiplied = eventDB.Home_Odd;
                    else if(endMatch.Team1Goals<endMatch.Team2Goals)
                        multiplied = eventDB.Away_Odd;
                    else 
                        multiplied = eventDB.Tie_Odd;
                    var user = await _appUserRepository.GetUserByIdAsync(bet.appUserId);
                    user.Balance = user.Balance + bet.value * multiplied;
                    _appUserRepository.UpdateUser(user);
                    if (!await _appUserRepository.SaveAllAsync())
                    {
                        return false;
                    }

                    string ip = user.IpNotification;
                    int port = user.PortNotification;
                    string description = "congratulations, you won " + bet.value;

                    // SEND TO CLIENT
                }


        }
        // atualiza o evento
        eventDB.eventStateId = 2;
        _eventRepository.Update(eventDB);
        if (!await _eventRepository.SaveAllAsync())
        {
            return false;
        }
        //remove o eventObservable
        _observables.RemoveEventObservable(eventObservable);

        return true;
    }

    [NonAction]
    public void SendNotification(string description, EventDB eventDB )
    {
        // using (var scope = _provider.CreateScope())
        //     {
        //         var observables = scope.ServiceProvider.GetRequiredService<IObservables>();
        //     }
        EventObservable eventObservable = _observables.GetEventObservableByIdEvent(eventDB.Id);

        eventObservable.update(description);
    }

}