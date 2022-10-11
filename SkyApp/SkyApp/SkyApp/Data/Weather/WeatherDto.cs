using System;

namespace SkyApp.Data.Weather;

public class WeatherDto
{
    public LocationResponseDto location { get; set; }
    public DetailedReportDto current { get; set; }
}