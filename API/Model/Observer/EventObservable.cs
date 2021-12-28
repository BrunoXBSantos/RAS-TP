using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;

// Cada evento com state = 1 é criado um objeto desta classe
public class EventObservable : IObservable<Notification>
{
    // lista dos clientes que apostaram no evento idEvent
    private List<IObserver<Notification>> observers;
    public int idEvent {get ; set ; }

    public EventObservable(int idEvent)
    {
        observers = new List<IObserver<Notification>>();
        this.idEvent = idEvent;
    }

    // Apostadores que pretendem receber notificaçoes 
    public IDisposable Subscribe(IObserver<Notification> observer)
    {
        // Check whether observer is already registered. If not, add it
        if (!observers.Contains(observer)) {
            observers.Add(observer);
            // Provide observer with existing data.
            // foreach (var item in flights)
            //     observer.OnNext(item);
        }
        return new Unsubscriber<Notification>(observers, observer);
    }
    
    public void update(string description){
        if(description != null){
            var info = new Notification(description);
            foreach(var observer in observers){
                observer.OnNext(info);
            }
        }
    }
}

internal class Unsubscriber<Notification> : IDisposable
{
   private List<IObserver<Notification>> _observers;
   private IObserver<Notification> _observer;

   internal Unsubscriber(List<IObserver<Notification>> observers, IObserver<Notification> observer)
   {
      this._observers = observers;
      this._observer = observer;
   }

   public void Dispose()
   {
      if (_observers.Contains(_observer))
         _observers.Remove(_observer);
   }
}