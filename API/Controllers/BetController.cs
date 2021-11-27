using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Entities.Bet;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("api/getAllBets")]
        [AllowAnonymous]
        public string GetAllBets()
        {
            //o namespace ToListAsync é != do que o ToList
            // obter results de 1 tarefa 
            return this.betsUpdated.getBets();
        }
        
    }
}