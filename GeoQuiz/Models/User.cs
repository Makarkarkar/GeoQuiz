using System.Text.Json.Serialization;

namespace GeoQuiz;

public class User
{
    [JsonPropertyName("userName")]
    public string? UserName { get; set; }
    [JsonPropertyName("guid")]
    public string? GUID { get; set; }
    [JsonPropertyName("count")]
    public int? Count { get; set; }
    [JsonPropertyName("lobbyGUID")]
    public string? LobbyGUID { get; set; }
}