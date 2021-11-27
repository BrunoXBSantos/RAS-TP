using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;

namespace API.Services
{
    public class BetsUpdated : IBetsUpdated
    {
        private string Bets;
        public BetsUpdated()
        {
            this.Bets = null;
        }

        public string getBets()
        {
            return this.Bets;
        }

        public void setBets(string bets)
        {
            this.Bets = bets;
        }

    }
}