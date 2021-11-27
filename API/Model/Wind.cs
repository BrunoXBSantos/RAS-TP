using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API.Model
{
    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public float speed { get; set; }
        [JsonProperty(PropertyName = "deg")]
        public int deg { get; set; }
    }
}