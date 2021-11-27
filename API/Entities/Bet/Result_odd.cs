using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Bet
{
    public class Result_odd
    {
        public int Id { get; set; }
        public float home { get; set; }
        public float tie { get; set; }
        public float away { get; set; }
    }
}