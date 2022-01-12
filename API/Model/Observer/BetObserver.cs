using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.AppNetUsers;
using API.Interfaces;

namespace API.Model;
public class BetObserver : IObserver<Notification>
{   
    public int betId { get; set; }
    public int eventId { get; set; }
    public int userId { get; set; }
    private readonly IServiceProvider _provider;

    private IDisposable cancellation;

    public BetObserver(int betId, int eventId, int userId, IServiceProvider provider)
    {
        this.betId = betId;
        this.eventId = eventId;
        this.userId = userId;
        _provider=  provider;
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public async void OnNext(Notification value)
    {
        using (var scope = _provider.CreateScope())
        {
            var _appUserRepository = scope.ServiceProvider.GetRequiredService<IAppUserRepository>();
            var activeNotificationSimpleDto = await _appUserRepository.GetActiveNotificationSimpleDtoIdAsync(this.userId);
            // Só se as notificaçoes estiverem ativas!!!!!!!!!
            if(activeNotificationSimpleDto.ActiveNotification){
                string Ip = activeNotificationSimpleDto.IpNotification;
                int port = activeNotificationSimpleDto.PortNotification;
                Console.WriteLine("NOTIFICACAO " +
                                        "  Ip: " +  Ip+
                                        "  Port: " + port
                                        + "\n\t" + value.description);
                ////// FALAPE COCOCAS AQUI O TEU CODIGO
            }

        }
        
    }

    public virtual void Subscribe(EventObservable eventObservable)
    {
        cancellation = eventObservable.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        cancellation.Dispose();
    }
}