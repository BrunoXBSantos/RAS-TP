using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Model;

namespace API.Services;

// Este serviço é singleton, e tem todos os EventObservable
// Cada evento com state=1 é um Observable
// logo, isto tem a lista de todos os eventos com state = 1
public class Observables : IObservables
{
    public List<EventObservable> eventObservables {get ; set ;}

    public Observables()
    {
        this.eventObservables = new List<EventObservable>();
        Console.WriteLine("teaaaaaaaaaaaaaaa");
    }

    public List<EventObservable> getAllEventObservable(){
        return this.eventObservables;
    }

    public void AddEventObservable(EventObservable eventObservable){
        this.eventObservables.Add(eventObservable);
    }

    public void RemoveEventObservable(EventObservable eventObservable){
        this.eventObservables.Remove(eventObservable);
    }

    // Obter o EventObserble do respetivo eventId
    public EventObservable GetEventObservableByIdEvent(int idEvent){
        EventObservable eventObservable = null;
        eventObservable = this.eventObservables.Find(e => e.idEvent == idEvent);
        return eventObservable;
    }

}