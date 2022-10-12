using System.Text.Json.Serialization;

namespace SkyApp.Data.Weather;

public class LocationResponseDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("region")]
    public string Region { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
    
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("tz_id")]
    public string TimeZoneName { get; set; }

    [JsonPropertyName("localtime")]
    public string LocalTime { get; set; }
}