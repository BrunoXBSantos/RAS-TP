using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Bet
{
    public class Event
    {
        public int Id { get; set; }
        public string sport { get; set; }
        public string type { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }

        public Result_odd result_odd { get; set; }
    }
}