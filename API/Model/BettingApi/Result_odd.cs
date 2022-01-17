using System.Text.Json.Serialization;

namespace API.Model;
public class Result_odd
{
    [JsonPropertyName("home")]
    public double home { get; set; }
    [JsonPropertyName("tie")]
    public double tie { get; set; }
    [JsonPropertyName("away")]
    public double away { get; set; }
}