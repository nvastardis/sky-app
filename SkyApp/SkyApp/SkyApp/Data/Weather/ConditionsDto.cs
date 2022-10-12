using System.Text.Json.Serialization;

namespace SkyApp.Data.Weather;

public class ConditionsDto
{
    [JsonPropertyName("text")]
    public string Details { get; set; }
    
    [JsonPropertyName("icon")]
    public string IconUrl { get; set; }
    
    [JsonPropertyName("code")]
    public int UniqueCode { get; set; }
}