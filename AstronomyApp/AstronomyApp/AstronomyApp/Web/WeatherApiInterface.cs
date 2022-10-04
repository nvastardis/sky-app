using System.Threading.Tasks;
using AstronomyApp.Data.Weather;

namespace AstronomyApp.Web;

public interface WeatherApiInterface
{
    Task<WeatherDto> GetWeather();
    Task<WeatherDto> GetWeather(string location);
}