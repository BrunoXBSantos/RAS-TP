using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model;
public class ListEventAll
{   
    [JsonPropertyName("listEventsAll")]
    public ICollection<EventsBettingApi> listEventsAll { get; set; }
}