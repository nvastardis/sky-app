namespace SkyApp.Data.Weather;

public class WeatherDto
{
    public LocationDto Location { get; set; }
    public WeatherReportDto Current { get; set; }
}