using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.BackgroundTask
{
     public class Background : IHostedService
    {
        private readonly ILogger<Background> logger;
        private readonly IBettingApi bettingApi;
        private readonly IServiceProvider _provider;

        public Background(ILogger<Background> logger,
            IBettingApi bettingApi,
            IServiceProvider provider)
        {
            this.logger = logger;
            this.bettingApi = bettingApi;
            _provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // atualiza a lista que possui a lista dos observers 
            await updateObservables();
            await updateObservers();
            await bettingApi.GetBetsAll(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Printing worker stopping");
            return Task.CompletedTask;
        }

        // vai ir à BD e ver se existem eventos com state=1, ou seja, eventos open
        // se existir cria o eventObservable respetivo e adiciona-o ao observables
        public async Task updateObservables(){
            using (var scope = _provider.CreateScope())
            {
                var observables = scope.ServiceProvider.GetRequiredService<IObservables>();
                var _eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                //obtem os eventos que estão open
                var events = await _eventRepository.GetEventsOpen();
                foreach(EventSimpleDto _event in events){
                    var eventObservable = new EventObservable(_event.Id);
                    observables.AddEventObservable(eventObservable);
                }
                
            }
        }

        public async Task updateObservers(){
            using (var scope = _provider.CreateScope())
            {
                var observables = scope.ServiceProvider.GetRequiredService<IObservables>();
                var _betRepository = scope.ServiceProvider.GetRequiredService<IBetRepository>();
                //obtem os eventos que estão open
                var bets = await _betRepository.GetBetsOpen();
                foreach(BetEmptyDto bet in bets){
                    EventObservable eventObservable = observables.GetEventObservableByIdEvent(bet._eventId);
                    BetObserver betObserver = new BetObserver(bet.Id, bet._eventId, bet.appUserId);
                    betObserver.Subscribe(eventObservable);
                }
                
            }
        }
    }
}