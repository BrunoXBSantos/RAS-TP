using API.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using AutoMapper.QueryableExtensions;
using API.DTOs;
using API.Helpers;

namespace API.Data;

public class EventRepository : IEventRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public EventRepository(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }


    #region CREATE
    // add a Event to the DB
    public void AddEvent(EventDB _event)
    {
        _context.DB_Events.Add(_event);
    }

    #endregion

    #region READ
    // get a ServerEmptyDTO by id
    public async Task<EventDisplayDto> GetEventAsync(int id)
    {
        return await _context.DB_Events
            .Where(x => x.Id == id)
            .ProjectTo<EventDisplayDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    // get a event detailed by id
    public async Task<EventSimpleDto> GetEventDetailAsync(int id)
    {
        var a = await _context.DB_Events
            .Where(x => x.Id == id)
            // .Include(s => s.Warnings)
            // .Include(s => s.Comments)
            .ProjectTo<EventSimpleDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        return a;
    }

    // get a state de um evento
    public async Task<int> GetEventStateByIdAsync(int id)
    {
        var _event = await _context.DB_Events
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        return _event.eventStateId;
    }

    // get events with state is open
    public async Task<List<EventSimpleDto>> GetEventsOpen()
    {
        var _events = await _context.DB_Events
            .Where(x => x.eventStateId == 1)
            .ProjectTo<EventSimpleDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return _events;
    }

    // get EventDisplayDto list by paging
    public async Task<PagedList<EventDisplayDto>> GetEventsAsync(EventParams eventParams)
    {
        var query = _context.DB_Events
            .ProjectTo<EventDisplayDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .AsQueryable();

        return await PagedList<EventDisplayDto>.CreateAsync(query, 
                eventParams.PageNumber, eventParams.PageSize,eventParams.FilterOptions2);
    }

    // get EventDisplayDto list by paging
    public async Task<EventDB> GetIdEventByParams(MatchDto startMatchDto)
    {   
        var result = _context.DB_Events
            .Include(a => a.sport)
            .Include(a => a.eventState)
            .SingleOrDefaultAsync(e => e.Team1 == startMatchDto.Team1 &&
                                       e.Team2 == startMatchDto.Team2 &&
                                       e.sport.Description.Equals(startMatchDto.Sport));
        
        return await result;
    }

    #endregion

    #region UPDATE

    public void Update(EventDB _event)
    {
        _context.Entry(_event).State = EntityState.Modified;
    }

    // get a ServerEmptyDTO by id
    // public async Task<EventDB> GetEventAsync(int id)
    // {
    //     return await _context.DB_Events
    //         .Where(x => x.Id == id)
    //         .ProjectTo<EventEmptyDto>(_mapper.ConfigurationProvider)
    //         .SingleOrDefaultAsync();
    // }

    // verifica se o evento já existe na BD
    // type -> tipo de evento "full time"
    // sport -> soccer p.e.
    public async Task<bool> checkEvent(int type, int sport, string team1, string team2)
    {   
        var check = false;

        check = await _context.DB_Events.AnyAsync(x => (
                x.Team1.Equals(team1) && 
                x.Team2.Equals(team2) &&
                x.sportId == sport    &&
                x.eventTypeId == type
        ));

        return check;
    }
    #endregion

    #region DELETE
    #endregion

    // checkEventExistByIdAsync id exist
    public async Task<Boolean> checkEventExistByIdAsync(int id){
        return await _context.DB_Events
            .AnyAsync(s => s.Id == id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}