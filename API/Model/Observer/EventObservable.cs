using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;

// Cada evento com state = 1 é criado um objeto desta classe
public class EventObservable
{
    // lista dos clientes que apostaram no evento idEvent
    public List<BetObserver> observers;
    public int idEvent {get ; set ; }

    public EventObservable(int idEvent)
    {
        observers = new List<BetObserver>();
        this.idEvent = idEvent;
    }

    // Apostadores que pretendem receber notificaçoes 
    public IDisposable Subscribe(BetObserver observer)
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
    
    public void update(Notification description){
        if(description != null){
            foreach(var observer in observers){
                observer.OnNext(description);
            }
        }
    }
}

internal class Unsubscriber<Notification> : IDisposable
{
   private List<BetObserver> _observers;
   private BetObserver _observer;

   internal Unsubscriber(List<BetObserver> observers, BetObserver observer)
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