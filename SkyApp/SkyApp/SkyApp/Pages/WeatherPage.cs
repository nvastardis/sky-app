using SkyApp.Web.Weather;
using Xamarin.Forms;

namespace SkyApp.Pages;

public class WeatherPage: ContentPage
{
    private readonly IWeatherApi _weatherApi;

    public WeatherPage(IWeatherApi api)
    {
        _weatherApi = api;
    }
}