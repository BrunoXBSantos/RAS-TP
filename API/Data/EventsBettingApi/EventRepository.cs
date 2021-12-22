using API.Model;
using AutoMapper;
using API.DTOs;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

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

    // add a Event to the DB
    public void AddEvent(EventDB _event)
    {
        _context.DB_Events.Add(_event);
    }

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


    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}