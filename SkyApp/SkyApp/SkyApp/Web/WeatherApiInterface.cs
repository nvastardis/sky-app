using System.Threading.Tasks;
using SkyApp.Data.Weather;

namespace SkyApp.Web;

public interface WeatherApiInterface
{
    Task<WeatherDto> GetWeather();
    Task<WeatherDto> GetWeather(string location);
}