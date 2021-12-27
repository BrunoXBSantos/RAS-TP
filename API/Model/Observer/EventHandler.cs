using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model;
public class EventHandler : IObservable<EventNotification>
{
    // lista dos clientes que apostaram no evento idEvent
    private List<IObserver<EventNotification>> observers;
    private int idEvent;

    public EventHandler(int idEvent)
    {
        observers = new List<IObserver<EventNotification>>();
        this.idEvent = idEvent;
    }

    // Apostadores que pretendem receber notificaçoes 
    public IDisposable Subscribe(IObserver<EventNotification> observer)
    {
        // Check whether observer is already registered. If not, add it
        if (!observers.Contains(observer)) {
            observers.Add(observer);
            // Provide observer with existing data.
            // foreach (var item in flights)
            //     observer.OnNext(item);
        }
        return new Unsubscriber<EventNotification>(observers, observer);
    }
}

internal class Unsubscriber<EventNotification> : IDisposable
{
   private List<IObserver<EventNotification>> _observers;
   private IObserver<EventNotification> _observer;

   internal Unsubscriber(List<IObserver<EventNotification>> observers, IObserver<EventNotification> observer)
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