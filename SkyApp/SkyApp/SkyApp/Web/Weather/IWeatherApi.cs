using System.Threading.Tasks;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public interface IWeatherApi
{
    Task<WeatherApiResponse> GetWeather(string location = null);
}