using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model.Football;

namespace API.Interfaces
{
        
    public interface IBetsUpdated
    {
        public ICollection<Event> getBets();
        public void setBets(ListEventAll events);
        
    }
}