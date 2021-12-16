using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Model.Football;

namespace API.Services
{
    public class BetsUpdated : IBetsUpdated
    {
        private ICollection<Event> events;
        public BetsUpdated()
        {
            this.events = null;
        }

        public ICollection<Event> getBets()
        {
            return this.events;
        }

        public void setBets(ListEventAll listEventAll)
        {
            this.events = this.update(listEventAll);
        }

        public ICollection<Event> update (ListEventAll listEventAll){
            ICollection<Event> events = new List<Event>();
            // var num = listEventAll.listEventsAll.Count();
            // for(int i = 0; i < num; i++){
            //     foreach(Events _events in listEventAll.listEventsAll){
            //         events.Add(_events._event);
            // }
            foreach(Events _events in listEventAll.listEventsAll){
                    events.Add(_events._event);
            }
            return events;
        }

    }
}