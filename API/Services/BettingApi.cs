using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Constants;
using API.Data;
using API.Interfaces;
using API.Model;
using API.RequestHandlers;
using Microsoft.Extensions.Logging;

namespace API.Services;
public class BettingApi : IBettingApi
{
    private readonly ILogger<BettingApi> _logger;
    // classe que contem as apostas 
    private readonly IBetsUpdated betsUpdated;
    //private IEventRepository _eventRepository;
    private readonly IServiceProvider _provider;

    private int number = 0;
    public BettingApi(ILogger<BettingApi> logger, IBetsUpdated betsUpdated,
            /*IEventRepository eventRepository*/
            IServiceProvider provider)
    {
        _logger = logger;
        this.betsUpdated = betsUpdated;
        _provider = provider;
    }

    public async Task GetBetsAll(CancellationToken cancellationToken)
    {
        IRequestHandler httpClientRequestHandler = new HttpClientRequestHandler();

        while (!cancellationToken.IsCancellationRequested)
        {   
            await Task.Delay(1000 * 10);

            Interlocked.Increment(ref number);
            _logger.LogInformation($"Worker printing number: {number}");

            var response = await httpClientRequestHandler.Get(BettingApiConstants.url);

            await updateDB_Events(response, _logger);

            var r = response + "\n " + $"Worker printing number: {number}";
            _logger.LogInformation(r);
            this.betsUpdated.setBets(response);


        }
    }

    // atualiza a tabela de eventos a partir da Betting Api
    public async Task updateDB_Events(ListEventAll listEventAll, ILogger<BettingApi> logger){
        using (var scope = _provider.CreateScope())
        {
            var _eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
            // do something with scoped service
            foreach(EventsBettingApi _events in listEventAll.listEventsAll){
                EventDB eventDB = new EventDB();
                eventDB.Team1 = _events._event.team1;
                eventDB.Team2 = _events._event.team2;
                eventDB.Home_Odd = _events._event.result_odd.home;
                eventDB.Tie_Odd = _events._event.result_odd.tie;
                eventDB.Away_Odd = _events._event.result_odd.away;

                if(_events._event.sport.Equals("Soccer"))
                    eventDB.sportId = 1;
                else if(_events._event.sport.Equals("Chess"))
                    eventDB.sportId = 2;
                
                if(_events._event.type.Equals("Full Time"))
                    eventDB.eventTypeId = 1;

                var check = await _eventRepository.checkEvent(eventDB.eventTypeId, eventDB.sportId, eventDB.Team1, eventDB.Team2);

                if(check)
                    _eventRepository.Update(eventDB);
                else {
                    eventDB.eventStateId = 2;
                    _eventRepository.AddEvent(eventDB);
                }

                if (await _eventRepository.SaveAllAsync()) 
                    _logger.LogInformation("Error to update event!");
            }
        }

    }
}