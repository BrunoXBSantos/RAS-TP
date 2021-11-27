using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Weather
{
    public class Weather
    {
        public int Id { get; set; }

        public Wind wind { get; set; }

        public int timezone { get; set; }
        public string name { get; set; }
        public int code { get; set; }
    }
}