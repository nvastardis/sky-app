using System;
using System.Text.Json.Serialization;

namespace SkyApp.Data.Weather;

public class DetailedReportDto
{

    [JsonPropertyName("last_updated")]
    public string LocalTimeOfLastUpdate { get; set; }
    
    [JsonPropertyName("temp_c")]
    public double TemperatureInCelsius { get; set; }
    
    [JsonPropertyName("temp_f")]
    public double TemperatureInFahrenheit { get; set; }
    
    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }
    
    [JsonPropertyName("condition")]
    public ConditionsDto Conditions { get; set; }
    
    [JsonPropertyName("wind_mph")]
    public double WindSpeedInMilesPerHour { get; set; }
    
    [JsonPropertyName("wind_kph")]
    public double WindSpeedInKilometersPerHour { get; set; }
    
    [JsonPropertyName("wind_degree")]
    public int WindDirectionInDegrees { get; set; }
    
    [JsonPropertyName("wind_dir")]
    public string WindDirectionInCompass { get; set; }
    
    [JsonPropertyName("pressure_mb")]
    public double PressureInMillibars { get; set; }
    
    [JsonPropertyName("pressure_in")]
    public double PressureInInches { get; set; }
    
    [JsonPropertyName("precip_mm")]
    public double PrecipitationInMillimeters { get; set; }
    
    [JsonPropertyName("precip_in")]
    public double PrecipitationInInches { get; set; }
    
    [JsonPropertyName("humidity")]
    public int HumidityPercentage { get; set; }
    
    [JsonPropertyName("cloud")]
    public int CloudCoverPercentage { get; set; }
    
    [JsonPropertyName("feelslike_c")]
    public double FeelsTempInCelsius { get; set; }
    
    [JsonPropertyName("feelslike_f")]
    public double FeelsTempInFahrenheit { get; set; }
    
    [JsonPropertyName("vis_km")]
    public double VisibilityInKilometers { get; set; }
    
    [JsonPropertyName("vis_miles")]
    public double VisibilityInMiles { get; set; }
    
    [JsonPropertyName("uv")]
    public double UvIndex { get; set; }
    
    [JsonPropertyName("gust_mph")]
    public double GustInMilesPerHour { get; set; }
    
    [JsonPropertyName("gust_kph")]
    public double GustInKilometersPerHour { get; set; }
    
}