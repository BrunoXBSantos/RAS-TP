using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Model;

namespace API.Interfaces;
public interface IEventRepository
{
    public void AddEvent(EventDB _event);

    public void Update(EventDB _event);

    public Task<EventDB> GetEventDBAsync(int id);

    public Task<EventDisplayDto> GetEventAsync(int id);
    public Task<PagedList<EventDisplayDto>> GetEventsWithStateAsync(EventParams eventParams, int state);
    Task<PagedList<EventDisplayDto>> GetEventsWithStateNotFinishedAsync(EventParams eventParams);

    public Task<EventSimpleDto> GetEventDetailAsync(int id);

    public Task<PagedList<EventDisplayDto>> GetEventsAsync(EventParams eventParams);
    public Task<List<EventSimpleDto>> GetEventsOpen();

    Task<EventDB> GetEventToBettingApiAsync(int idType, int idSport, string team1, string team2);

    public Task<EventDB> GetIdEventByParams(MatchDto startMatchDto);

    Task<bool> checkEvent(int type, int sport, string team1, string team2);
    public Task<Boolean> checkEventExistByIdAsync(int id);
    public Task<int> GetEventStateByIdAsync(int id);

    public Task<bool> SaveAllAsync();
    
}