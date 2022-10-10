namespace SkyApp.Data.Weather;

public class WeatherDto
{
    public LocationResponseDto Location { get; set; }
    public DetailedReportDto Current { get; set; }
}