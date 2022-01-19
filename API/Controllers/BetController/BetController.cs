using System.Collections.Generic;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

//[Authorize]
public class BetController : BaseApiController
{
    private readonly IBetRepository _betRepository;
    private readonly IMapper _mapper;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IObservables _observables;
    private readonly IEventRepository _eventRepository;
    private readonly IWalletRepository _walletRepository;

    private readonly IServiceProvider _provider;
    public BetController(IBetRepository betRepository,
                         IEventRepository eventRepository,
                         IMapper mapper,
                         IAppUserRepository appUserRepository,
                         IWalletRepository walletRepository,
                         IObservables observables,
                         IServiceProvider provider)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _appUserRepository = appUserRepository;
        _walletRepository = walletRepository;
        _observables = observables;
        _betRepository = betRepository;
        _provider = provider;
    }

    #region Create 
    /// <summary>
    /// POST a especific server. Create a new server.
    /// </summary>
    /// <returns> The newly created Server </returns>
    [HttpPost]
    public async Task<ActionResult<BetEmptyDto>> CreateBet(CreateBetDTO createBetDTO)
    {
        
        if(!(await _eventRepository.checkEventExistByIdAsync(createBetDTO._eventId))){
            return BadRequest("Event does not Exist!");
        }

        var eventState = await _eventRepository.GetEventStateByIdAsync(createBetDTO._eventId);
         // Não dá para apostar
        if(eventState == 2){
            return Unauthorized("Event has already ended.");
        }
        else if(eventState == 3){
            return Unauthorized("Event is suspended.");
        }

        WalletCoin walletCoin = await _walletRepository.GetWalletCoinAsync(createBetDTO.appUserId, createBetDTO.coinID);
        // verifica se tem money suficiente para apostar
        if(walletCoin == null){
            return NotFound("User does not have that currency.");
        }
        
        if(createBetDTO.value > walletCoin.Balance)
            return Unauthorized("Insufficient funds.");

        // se existir atualizo
        walletCoin.Balance -= createBetDTO.value;
        _walletRepository.Update(walletCoin);
        if (!await _walletRepository.SaveAllAsync()){ 
            return BadRequest("Error processing bet");
        }

        var bet = _mapper.Map<Bet>(createBetDTO);
        bet.betStateId = 1;

        _betRepository.AddBet(bet);
        if (!await _betRepository.SaveAllAsync()) 
            return BadRequest("Failed to create Bet");

        int lastBet =  _betRepository.GetNumberBets();    

        //crio o observer
        BetObserver betObserver = new BetObserver(lastBet, bet._eventId, bet.appUserId);
        EventObservable eventObservable = _observables.GetEventObservableByIdEvent(bet._eventId);
        betObserver.Subscribe(eventObservable);
        
        return Ok(_mapper.Map<BetEmptyDto>(bet));

    }
    #endregion

    #region Read
    /// <summary>
    /// Get a especific BetEmptyDto by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BetEmptyDto>> GetBetEmptyByIdAsync(int id)
    {
        var bet = new BetEmptyDto();
        bet = await _betRepository.GetBetEmptyByIdAsync(id);
        if (bet == null) return NotFound("Bet not found");
        return Ok(bet);
    }

    /// <summary>
    /// Get a especific BetSimpleDto by id. Bet more detailed.
    /// </summary>
    [HttpGet("{id}/detailed")]
    public async Task<ActionResult<BetSimpleDto>> GetBetSimpleByIdAsync(int id)
    {
        var bet = await _betRepository.GetBetSimpleByIdAsync(id);
        if (bet == null) return BadRequest("Bet not found");
        return Ok(bet);
    }

    /// <summary>
    /// Gets the list of bets from a given username.
    /// </summary>
    [HttpGet("user/idOrUsername")]
    public async Task<ActionResult<IEnumerable<BetEmptyDto>>> GetBetsEmptyByUserName([FromQuery]BetParams betParams, string idOrUsername)
    {   
        var user = new AppUser();
        if (IsDigitsOnly(idOrUsername))
        {
            var userx = await _appUserRepository.GetByIdUserAsync(idOrUsername);
            user = await _appUserRepository.GetUserByUsernameAsync(userx.username);
        }
        else { user = await _appUserRepository.GetUserByUsernameAsync(idOrUsername); }
        

        if(user == null){
            return NotFound("User not found");
        }            
        var bets = await _betRepository.GetBetsSimpleByIdAsync(betParams, user.Id);

        Response.AddPaginationHeader(bets.CurrentPage, bets.PageSize, 
            bets.TotalCount, bets.TotalPages);

        return Ok(bets);
    }

    /// <summary>
    /// Gets the list of bets with state is open.
    /// </summary>
    [HttpGet("Open")]
    public async Task<ActionResult<IEnumerable<BetSimpleDto>>> GetBetsOpen([FromQuery]BetParams betParams)
    {   
        var bets = await _betRepository.GetBetsWithState(betParams, 1);

        Response.AddPaginationHeader(bets.CurrentPage, bets.PageSize, 
            bets.TotalCount, bets.TotalPages);

        return Ok(bets);
    }

    /// <summary>
    /// Gets the list of bets with state is Suspended.
    /// </summary>
    [HttpGet("Suspended")]
    public async Task<ActionResult<IEnumerable<BetSimpleDto>>> GetBetsSuspended([FromQuery]BetParams betParams)
    {   
        var bets = await _betRepository.GetBetsWithState(betParams, 3);

        Response.AddPaginationHeader(bets.CurrentPage, bets.PageSize, 
            bets.TotalCount, bets.TotalPages);

        return Ok(bets);
    }

    /// <summary>
    /// Gets the list of bets with state is Suspended.
    /// </summary>
    [HttpGet("Finished")]
    public async Task<ActionResult<IEnumerable<BetSimpleDto>>> GetBetsFinished([FromQuery]BetParams betParams)
    {   
        var bets = await _betRepository.GetBetsWithState(betParams, 2);

        Response.AddPaginationHeader(bets.CurrentPage, bets.PageSize, 
            bets.TotalCount, bets.TotalPages);

        return Ok(bets);
    }
    #endregion

    #region Delete
    /// <summary>
    /// Apagar uma bet de um user
    /// </summary>
    [HttpDelete("/{idUser}/{idBet}")]
    public async Task<ActionResult> DeleteBet(int idUser, int idBet)
    {
        var bet = await _betRepository.GetBetByIdAsync(idBet);
        if(bet == null)
            return NotFound("Bet Not Found.");

        if(bet.betStateId == 2)
            return Unauthorized("Bet closed.");

        var eventDB = await _eventRepository.GetEventDBAsync(bet._eventId);
        if(eventDB == null)
            return NotFound("Bet Not Found.");
        
        if(eventDB.eventStateId == 2){
            return BadRequest("Event is over.");
        }
        // se o jogo já começou
        if(eventDB.eventStateId == 4){
            return Unauthorized("Event has already started. You cannot cancel the bet.");
        }

        EventObservable eventObservable = _observables.GetEventObservableByIdEvent(bet._eventId);
        BetObserver betObserver = eventObservable.observers.Find(e => e.betId == idBet);
        
        if(betObserver != null){
            eventObservable.observers.Remove(betObserver);
        }


        // deposito 0,75% do dinheiro apostado
        WalletCoin walletCoin = await _walletRepository.GetWalletCoinAsync(bet.appUserId, bet.coinID);
        walletCoin.Balance = walletCoin.Balance + bet.value*0.75;

        _walletRepository.Update(walletCoin);
        if(! await _walletRepository.SaveAllAsync()){
            return BadRequest("Problem deleting the bet");
        }

        // colocar bet terminada
        bet.betStateId = 2;
        _betRepository.UpdateBet(bet);
        if(! await _betRepository.SaveAllAsync()){
            return BadRequest("Problem deleting the bet");
        }

        return NoContent();
    }
    #endregion


    [NonAction]
    bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }
        
}