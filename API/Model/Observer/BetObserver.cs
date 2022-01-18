using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.AppNetUsers;
using API.Interfaces;
using API.WebSocketManager;

namespace API.Model;
public class BetObserver
{   
    public int betId { get; set; }
    public int eventId { get; set; }
    public int userId { get; set; }

    private IDisposable cancellation;

    public BetObserver(int betId, int eventId, int userId)
    {
        this.betId = betId;
        this.eventId = eventId;
        this.userId = userId;
    }

    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public async void OnNext(Notification sms)
    {
            var _appUserRepository = sms.provider.GetRequiredService<IAppUserRepository>();
            var activeNotificationSimpleDto = await _appUserRepository.GetActiveNotificationSimpleDtoIdAsync(this.userId);
            
            var dict_sockets = sms.provider.GetRequiredService<WebSocketServerConnectionManager>();

            await dict_sockets.SendMessageAsync(sms.description, userId.ToString());

            // // Só se as notificaçoes estiverem ativas!!!!!!!!!
            // if(activeNotificationSimpleDto.ActiveNotification){
                
            //     ////// FALAPE COCOCAS AQUI O TEU CODIGO
            // }
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