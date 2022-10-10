using System;
using System.Diagnostics;
using System.Text.Json;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SkyApp.Data.LocationFinder;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WebClient:
    IWeatherApi
{
    private readonly HttpClient _client;
    private readonly ILocationFinder _geoLocator;
    private readonly string _apiBaseUrl = "https://weatherapi-com.p.rapidapi.com/current.json?";

    public WebClient(ILocationFinder geoLocator)
    {
        _client = new();
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Test");
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        _geoLocator = geoLocator;
    }
    
    
    public async Task<WeatherApiResponse> GetWeather(string location = null)
    {
        
        var queryParameters = await SetQueryParametersViaGeoLocator(location);
        var result = new WeatherApiResponse();

        if (queryParameters.locationStatus != LocationFinderStatus.Success)
        {
            result.Weather = null;
            switch (queryParameters.locationStatus)
            {
                case LocationFinderStatus.PermissionException:
                    result.Status = WeatherApiResponseStatus.ErrorFindingLocationPermission;
                    break;
                case LocationFinderStatus.FeatureNotEnabledException:
                    result.Status = WeatherApiResponseStatus.ErrorFindingLocationFeatureNotEnabled;
                    break;
                case LocationFinderStatus.FeatureNotSupportedException:
                    result.Status = WeatherApiResponseStatus.ErrorFindingLocationFeatureNotSupported;
                    break;
                default:
                    result.Status = WeatherApiResponseStatus.Undefined;
                    break;
            }

            return result;

        }
        
        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new($"{_apiBaseUrl}{queryParameters.locationParameter}")
        };
        using var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStreamAsync();
        result.Weather = await JsonSerializer.DeserializeAsync<WeatherDto>(body);
        result.Status = WeatherApiResponseStatus.Success;
        return result;
    }
    
    private async Task<(string locationParameter,  LocationFinderStatus locationStatus)> SetQueryParametersViaGeoLocator(string location = null)
    {
        var query = "q=";
        if (location is not null)
        {
            return ($"{query}{location}", LocationFinderStatus.Success);
        }
        
        var currentLocation = await _geoLocator.GetCurrentLocationAsync();
        if (currentLocation.LocationFound is null)
        {
            return (null, currentLocation.ResponseStatus);
        }

        var locationParams =
            $"{currentLocation.LocationFound.Latitude.ToString(CultureInfo.InvariantCulture)},{currentLocation.LocationFound.Longitude.ToString(CultureInfo.InvariantCulture)}";
        
        return ($"{query}{locationParams}", LocationFinderStatus.Success);
    }
}