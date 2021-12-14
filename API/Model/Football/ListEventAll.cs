using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model.Football
{
    public class ListEventAll
    {   
        [JsonPropertyName("listEventsAll")]
        public ICollection<Events> listEventsAll { get; set; }
    }
}