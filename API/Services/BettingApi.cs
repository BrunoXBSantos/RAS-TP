using API.Constants;
using API.Interfaces;
using API.Model;
using API.RequestHandlers;

namespace API.Services;
public class BettingApi : IBettingApi
{
    private readonly ILogger<BettingApi> _logger;
    // classe que contem as apostas 
    private readonly IServiceProvider _provider;

    private int number = 0;
    public BettingApi(ILogger<BettingApi> logger,
            IServiceProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    public async Task GetBetsAll(CancellationToken cancellationToken)
    {
        IRequestHandler httpClientRequestHandler = new HttpClientRequestHandler();

        while (!cancellationToken.IsCancellationRequested)
        {   

            Interlocked.Increment(ref number);
            _logger.LogInformation($"Worker printing number: {number}");

            var response = await httpClientRequestHandler.Get(BettingApiConstants.url);

            await updateDB_Events(response, _logger);
            
            await Task.Delay(1000 * 10);
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

                if(_events._event.sport.Equals("soccer"))
                    eventDB.sportId = 1;
                else if(_events._event.sport.Equals("chess"))
                    eventDB.sportId = 2;
                
                if(_events._event.type.Equals("full time"))
                    eventDB.eventTypeId = 1;

                // verifica se o evento existe
                var check = await _eventRepository.checkEvent(eventDB.eventTypeId, eventDB.sportId, eventDB.Team1, eventDB.Team2);

                if(check){

                    EventDB eventDB1 = await _eventRepository.GetEventToBettingApiAsync(eventDB.eventTypeId, eventDB.sportId, eventDB.Team1, eventDB.Team2);
                    if(eventDB1 != null){

                        if( eventDB1.Home_Odd != eventDB.Home_Odd ||
                            eventDB1.Tie_Odd != eventDB.Tie_Odd ||
                            eventDB1.Away_Odd != eventDB.Away_Odd){

                            eventDB1.Home_Odd = eventDB.Home_Odd;
                            eventDB1.Tie_Odd = eventDB.Tie_Odd;
                            eventDB1.Away_Odd = eventDB.Away_Odd;

                            _eventRepository.Update(eventDB1);
                            if (await _eventRepository.SaveAllAsync()){
                                Console.WriteLine("Eventos atualizados");
                            }
                        }
                        
                    }
                }
                else { // se o evento nao existir na base de dados
                    eventDB.eventStateId = 1;
                    _eventRepository.AddEvent(eventDB);
                    if (!(await _eventRepository.SaveAllAsync())) 
                        _logger.LogInformation("Error to update event!");
                    
                    // crio um eventObservable deste evento, que vai receber os observers que fazem as apostas aqui
                    var eventObservable = new EventObservable(eventDB.Id);
                
                    var observables = scope.ServiceProvider.GetRequiredService<IObservables>();
                    observables.AddEventObservable(eventObservable);
                }
            }
        }
    }
}