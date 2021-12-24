using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;

namespace API.Interfaces
{
    public interface IEventRepository
    {
        public void AddEvent(EventDB _event);

        public void Update(EventDB _event);


        Task<bool> checkEvent(int type, int sport, string team1, string team2);

        public Task<bool> SaveAllAsync();
        
    }
}