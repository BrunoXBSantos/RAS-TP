using System.Text.Json.Serialization;

namespace API.Model;
public class EventBettingApi
{
    [JsonPropertyName("sport")]
    public string sport { get; set; }

    [JsonPropertyName("type")]
    public string type { get; set; }

    [JsonPropertyName("team1")]
    public string team1 { get; set; }

    [JsonPropertyName("team2")]
    public string team2 { get; set; }

    [JsonPropertyName("result_odd")]
    public Result_odd result_odd { get; set; }
}