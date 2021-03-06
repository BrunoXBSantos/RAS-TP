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
public class EventController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IEventRepository _eventRepository;
    public EventController(IMapper mapper, IEventRepository eventRepository)
    {
            _eventRepository = eventRepository;
            _mapper = mapper;

    }

    #region Create 
    #endregion

    #region Read 
    /// <summary>
    /// Get a especific Event by Id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EventDisplayDto>> GetEvent(int id)
    {
        var _event =  await _eventRepository.GetEventAsync(id);
        if (_event != null) return Ok(_event);
        return NotFound("Event not found");
    }

    /// <summary>
    /// For a given server id, get information about the server, your warnings and your comments. 
    /// </summary>
    /// /// <response code="201">Returns the Server id</response>
    /// <response code="404">If Server Not Found</response>
    [HttpGet("{id}/detailed")]
    public async Task<ActionResult<EventSimpleDto>> GetEventDetail(int id)
    {
        var eventEmpty = await _eventRepository.GetEventDetailAsync(id);
        if (eventEmpty != null) 
            return Ok(eventEmpty);
        return NotFound("Event not found");
    }

    /// <summary>
    /// Get the list of Events. Only event information, EventDisplayDto. 
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventDisplayDto>>> GetEvents([FromQuery]EventParams eventParams)
    {
        var events = await _eventRepository.GetEventsAsync(eventParams);

        Response.AddPaginationHeader(events.CurrentPage, events.PageSize, 
            events.TotalCount, events.TotalPages);

        return Ok(events);
    }

    /// <summary>
    /// Get the list of Events Finished. Only event information, EventDisplayDto. 
    /// </summary>
    [HttpGet("Finished")]
    public async Task<ActionResult<IEnumerable<EventDisplayDto>>> GetEventsFinished([FromQuery]EventParams eventParams)
    {
        var events = await _eventRepository.GetEventsWithStateAsync(eventParams, 2);

        Response.AddPaginationHeader(events.CurrentPage, events.PageSize, 
            events.TotalCount, events.TotalPages);

        return Ok(events);
    }

    /// <summary>
    /// Get the list of Events Suspended. Only event information, EventDisplayDto. 
    /// </summary>
    [HttpGet("Suspended")]
    public async Task<ActionResult<IEnumerable<EventDisplayDto>>> GetEventsSuspended([FromQuery]EventParams eventParams)
    {
        var events = await _eventRepository.GetEventsWithStateAsync(eventParams, 3);

        Response.AddPaginationHeader(events.CurrentPage, events.PageSize, 
            events.TotalCount, events.TotalPages);

        return Ok(events);
    }

    /// <summary>
    /// Get the list of Events Open. Only event information, EventDisplayDto. 
    /// </summary>
    [HttpGet("Open")]
    public async Task<ActionResult<IEnumerable<EventDisplayDto>>> GetEventsOpen([FromQuery]EventParams eventParams)
    {
        var events = await _eventRepository.GetEventsWithStateAsync(eventParams, 1);

        Response.AddPaginationHeader(events.CurrentPage, events.PageSize, 
            events.TotalCount, events.TotalPages);

        return Ok(events);
    }

    /// <summary>
    /// Get the list of Events Not Finished. Only event information, EventDisplayDto. 
    /// </summary>
    [HttpGet("NotFinished")]
    public async Task<ActionResult<IEnumerable<EventDisplayDto>>> GetEventsNotFinished([FromQuery]EventParams eventParams)
    {
        var events = await _eventRepository.GetEventsWithStateNotFinishedAsync(eventParams);

        Response.AddPaginationHeader(events.CurrentPage, events.PageSize, 
            events.TotalCount, events.TotalPages);

        return Ok(events);
    }


    #endregion

    #region Update 

    /// <summary>
    /// Colocar um dado evento no estado suspenso. 
    /// </summary>
    [HttpGet("{id}/suspend")]
    public async Task<ActionResult<EventSimpleDto>> updateEvent2suspend(int id)
    {   
        // 3 - Suspended      1 - Open
        var eventDB = await _eventRepository.GetEventDBAsync(id);
        if (eventDB != null){ 
            eventDB.eventStateId = 3;
            _eventRepository.Update(eventDB);
            if(await _eventRepository.SaveAllAsync())
                return Ok(_mapper.Map<EventSimpleDto>(eventDB));
            return BadRequest("Error to suspense the event");
        }
        return NotFound("Event not found");
    }

    /// <summary>
    /// Colocar um dado evento no estado open. 
    /// </summary>
    [HttpGet("{id}/open")]
    public async Task<ActionResult<EventSimpleDto>> updateEvent2open(int id)
    {   
        // 3 - Suspended      1 - Open
        var eventDB = await _eventRepository.GetEventDBAsync(id);
        if (eventDB != null){ 
            eventDB.eventStateId = 1;
            _eventRepository.Update(eventDB);
            if(await _eventRepository.SaveAllAsync())
                return Ok(_mapper.Map<EventSimpleDto>(eventDB));
            return BadRequest("Error to open the event");
        }
        return NotFound("Event not found");
    }


    #endregion

    #region Delete 
    #endregion
}