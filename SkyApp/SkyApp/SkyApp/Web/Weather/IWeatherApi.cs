using System.Threading.Tasks;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public interface IWeatherApi
{
    Task<WeatherDto> GetWeather();
    Task<WeatherDto> GetWeather(string location);
}