using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Model.Football
{
    public class Result_odd
    {
        [JsonPropertyName("home")]
        public float home { get; set; }
        [JsonPropertyName("tie")]
        public float tie { get; set; }
        [JsonPropertyName("away")]
        public float away { get; set; }
    }
}