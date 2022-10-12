using System;
using System.Text.Json.Serialization;

namespace SkyApp.Data.Weather;

public class WeatherDto
{
    [JsonPropertyName("location")]
    public LocationResponseDto CurrentLocation { get; set; }
    
    [JsonPropertyName("current")]
    public DetailedReportDto WeatherReport { get; set; }
}