using API.Interfaces;
using API.Model;

namespace API.Services
{
    public class BetsUpdated : IBetsUpdated
    {
        private ICollection<EventBettingApi> events;
        public BetsUpdated()
        {
            this.events = null;
        }

        public ICollection<EventBettingApi> getBets()
        {
            return this.events;
        }

        public void setBets(ListEventAll listEventAll)
        {
            this.events = this.update(listEventAll);
        }

        public ICollection<EventBettingApi> update (ListEventAll listEventAll){
            ICollection<EventBettingApi> events = new List<EventBettingApi>();
            // var num = listEventAll.listEventsAll.Count();
            // for(int i = 0; i < num; i++){
            //     foreach(Events _events in listEventAll.listEventsAll){
            //         events.Add(_events._event);
            // }
            foreach(EventsBettingApi _events in listEventAll.listEventsAll){
                    events.Add(_events._event);
            }
            return events;
        }

    }
}