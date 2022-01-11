using API.DTOs;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers;

public class MatchController : BaseApiController
{
    private readonly IEventRepository _eventRepository;

    private readonly IServiceProvider _provider;
    private readonly IObservables _observables;

    public MatchController(IEventRepository eventRepository, IServiceProvider provider, IObservables observables)
    {
        _eventRepository = eventRepository;
        _provider = provider;
        _observables = observables;
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
    public async Task<ActionResult<MatchDto>> EnfMatch(MatchDto endMatch)
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
                            " - " + endMatch.Team2 + 
                            " End Match";

        SendNotification(description, eventDb);
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