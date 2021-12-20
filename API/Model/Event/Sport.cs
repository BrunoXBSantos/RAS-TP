using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class Sport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<EventDB> events { get; set; }
    }
}