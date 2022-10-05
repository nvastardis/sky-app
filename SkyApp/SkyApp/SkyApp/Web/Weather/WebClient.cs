using System.Text.Json;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using SkyApp.Data;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WebClient:
    IWeatherApi
{
    private readonly HttpClient _client;
    private readonly LocationFinder geoLocator;
    private readonly string _apiBaseUrl = "https://weatherapi-com.p.rapidapi.com/current.json?";
    public WebClient()
    {
        _client = new();
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Test");
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        geoLocator = new ();
    }


    public async Task<WeatherDto> GetWeather()
    {
        var currentLocation = await geoLocator.GetCurrentLocationAsync();

        if (currentLocation is not null)
        {
            var queryParameters =
                $"q={currentLocation.Latitude.ToString(CultureInfo.InvariantCulture)},{currentLocation.Longitude.ToString(CultureInfo.InvariantCulture)}";
            using HttpRequestMessage request = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new($"{_apiBaseUrl}{queryParameters}")
            };
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<WeatherDto>(body);
                return result;
            }

        }
 

        return null;
    }

    public async Task<WeatherDto> GetWeather(string location)
    {
        throw new System.NotImplementedException();
    }
}