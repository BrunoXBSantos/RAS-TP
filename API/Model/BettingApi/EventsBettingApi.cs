using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model;
public class EventsBettingApi
{
    [JsonPropertyName("event")]
    public EventBettingApi _event { get; set; }
}
