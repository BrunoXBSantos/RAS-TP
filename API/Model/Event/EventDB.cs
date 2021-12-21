using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class EventDB
    {
        public int Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }

        public float Home_Odd { get; set; }
        public float Tie_Odd { get; set; }
        public float Away_Odd { get; set; }

        // Table EventType
        public EventType eventType { get; set; }
        public int eventTypeId { get; set; }

        // Table Sport
        public Sport sport { get; set; }
        public int sportId { get; set; }

        // Table Bet
        public ICollection<Bet> bets { get; set; }

    }
}