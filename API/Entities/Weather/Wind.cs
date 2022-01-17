using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Weather
{
    public class Wind
    {
        public int Id { get; set; }
        public double speed { get; set; }
        public double deg { get; set; }
    }
}