using System.Collections.Generic;
using API.Data;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class BetController : BaseApiController
{
    private readonly IBetRepository _betRepository;
    private readonly IMapper _mapper;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IObservables _observables;
    private readonly IEventRepository _eventRepository;
    public BetController(IBetRepository betRepository,
                         IEventRepository eventRepository,
                         IMapper mapper,
                         IAppUserRepository appUserRepository,
                         IObservables observables)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _appUserRepository = appUserRepository;
        _observables = observables;
        _betRepository = betRepository;
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

        if(!(await _appUserRepository.checkBalanceById(createBetDTO.appUserId, createBetDTO.value)))
            return Unauthorized("Insufficient funds.");

        var eventState = await _eventRepository.GetEventStateByIdAsync(createBetDTO._eventId);
        if(eventState != 1){  // Não dá para apostar
            if(eventState == 2){
                return Unauthorized("Event has already ended.");
            }
            else if(eventState == 3){
                return Unauthorized("Event is suspended.");
            }
            else 
                return BadRequest("Error checking event status");
        }


        var bet = _mapper.Map<Bet>(createBetDTO);
        bet.betStateId = 1;

        _betRepository.AddBet(bet);

        //crio o observer
        BetObserver betObserver = new BetObserver(bet.Id, bet._eventId, bet.appUserId);
        EventObservable eventObservable = _observables.GetEventObservableByIdEvent(bet._eventId);
        betObserver.Subscribe(eventObservable);
        
        Console.WriteLine("Aqui metia a cena naquilo broooooo");

        if (await _betRepository.SaveAllAsync()) 
            return Ok(_mapper.Map<BetEmptyDto>(bet));

        return BadRequest("Failed to create Bet");

    }
    #endregion

    #region Read
    /// <summary>
    /// Get a especific BetEmptyDto by id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BetEmptyDto>> GetBetEmpty(string id)
    {
        var bet = new BetEmptyDto();
        bet = await _betRepository.GetBetEmptyByIdAsync(id);
        if (bet == null) return BadRequest("Bet not found");
        return Ok(bet);
    }

    /// <summary>
    /// Get a especific BetSimpleDto by id. Bet more detailed.
    /// </summary>
    [HttpGet("{id}/detailed")]
    public async Task<ActionResult<BetSimpleDto>> GetBetSimple(string id)
    {
        var bet = new BetSimpleDto();
        bet = await _betRepository.GetBetSimpleByIdAsync(id);
        if (bet == null) return BadRequest("Bet not found");
        return Ok(bet);
    }

    /// <summary>
    /// Gets the list of bets from a given user id.
    /// </summary>
    /// /// <response code="200">Returns list of comments</response>
    /// <response code="400">If the item is null</response>
    [HttpGet("User={idUser}")]
    public async Task<ActionResult<IEnumerable<BetSimpleDto>>> GetBetsEmptyByUserId([FromQuery]BetParams betParams, string idUser)
    {   
        // verifica se o server existe
        if(!(await _appUserRepository.checkUserExistByIdAsync(idUser)))
            return NotFound("User Not Found");
            
        var bets = await _betRepository.GetBetSimpleByIdUserAsync(betParams, idUser);

        Response.AddPaginationHeader(bets.CurrentPage, bets.PageSize, 
            bets.TotalCount, bets.TotalPages);

        return Ok(bets);
    }
    #endregion
        
}