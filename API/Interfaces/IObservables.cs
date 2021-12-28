using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Interfaces;
public interface IObservables
{
    public List<EventObservable> getAllEventObservable();

    public void AddEventObservable(EventObservable eventObservable);
    public EventObservable GetEventObservableByIdEvent(int idEvent);
}
