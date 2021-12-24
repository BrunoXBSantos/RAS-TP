using System.Text.Json.Serialization;

namespace API.Model;
public class Result_odd
{
    [JsonPropertyName("home")]
    public float home { get; set; }
    [JsonPropertyName("tie")]
    public float tie { get; set; }
    [JsonPropertyName("away")]
    public float away { get; set; }
}