using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WeatherApiResponse
{
    public WeatherDto Weather;
    public WeatherApiResponseStatus Status;
}