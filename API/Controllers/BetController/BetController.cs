using System.Collections.Generic;
using API.Data;
using API.DTOs;
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
    private readonly IEventRepository _eventRepository;
    public BetController(IBetRepository betRepository,
                         IEventRepository eventRepository,
                         IMapper mapper,
                         IAppUserRepository appUserRepository)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _appUserRepository = appUserRepository;
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

        _betRepository.AddBet(bet);


        if (await _betRepository.SaveAllAsync()) 
            return Ok(_mapper.Map<BetEmptyDto>(bet));

        return BadRequest("Failed to create Bet");

    }
    #endregion
        
}