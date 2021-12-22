using API.Model;

namespace API.Interfaces
{

    public interface IBetsUpdated
    {
        public ICollection<EventBettingApi> getBets();
        public void setBets(ListEventAll events);
        
    }
}