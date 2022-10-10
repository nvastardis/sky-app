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
    private readonly LocationFinder _geoLocator;
    private readonly string _apiBaseUrl = "https://weatherapi-com.p.rapidapi.com/current.json?";

    public WebClient()
    {
        _client = new();
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Test");
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        _geoLocator = new();
    }
    
    
    public async Task<WeatherDto> GetWeather(string location = null)
    {
        var locationParameters = await SetQueryParametersViaGeoLocator(location);

        if (locationParameters is null)
        {
            return new();
        }
        
        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new($"{_apiBaseUrl}{locationParameters}")
        };
        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<WeatherDto>(body);
        return result;
    }
    
    private async Task<string> SetQueryParametersViaGeoLocator(string location = null)
    {
        var query = "q=";
        if (location is not null)
        {
            return $"{query}{location}";
        }
        var currentLocation = await _geoLocator.GetCurrentLocationAsync();
        
        if (currentLocation is null)
        {
            return null;
        }

        var locationParams =
            $"{currentLocation.Latitude.ToString(CultureInfo.InvariantCulture)},{currentLocation.Longitude.ToString(CultureInfo.InvariantCulture)}";
        
        return $"{query}{locationParams}";
    }
}