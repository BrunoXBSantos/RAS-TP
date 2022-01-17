using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Bet
{
    public class Result_odd
    {
        public int Id { get; set; }
        public double home { get; set; }
        public double tie { get; set; }
        public double away { get; set; }
    }
}