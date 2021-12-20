using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class Bet
    {
        public int Id { get; set; }
        public float value { get; set; }

        // Table AppUser
        public AppUser appUser { get; set; }
        public int? appUserId { get; set; }

        // table BetState
        public BetState betState { get; set; }
        public int? betStateId { get; set; }

        // table Event
        public EventDB _event { get; set; }
        public int? _eventId { get; set; }


    }
}