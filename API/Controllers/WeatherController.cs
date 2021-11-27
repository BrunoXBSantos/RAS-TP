using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities.Weather;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class WeatherController
    {
        private readonly DataContext _context;

        public WeatherController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Weather>>> GetAllBets()
        {
            //o namespace ToListAsync é != do que o ToList
            // obter results de 1 tarefa 
            return await _context.WeatherAtual.ToListAsync();
        }
    }
}