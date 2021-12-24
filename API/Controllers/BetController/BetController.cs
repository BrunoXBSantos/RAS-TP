using System.Collections.Generic;
using API.Data;
using API.DTOs;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BetController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IBetsUpdated betsUpdated;

        public BetController(DataContext context, IBetsUpdated betsUpdated)
        {
            _context = context;
            this.betsUpdated = betsUpdated;
        }

        [HttpGet]
        [Route("api/listBets")]
        public ICollection<EventBettingApi> listBets()
        {
            //o namespace ToListAsync é != do que o ToList
            // obter results de 1 tarefa 
            return  this.betsUpdated.getBets();
        }
        
    }
}