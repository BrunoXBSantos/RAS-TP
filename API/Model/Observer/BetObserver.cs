using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;
public class BetObserver : IObserver<Notification>
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

    public void OnNext(Notification value)
    {
        Console.WriteLine("NOTIFICACAO A ENVIAR PARA O FRONTEND" + value.description);
        throw new NotImplementedException();
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