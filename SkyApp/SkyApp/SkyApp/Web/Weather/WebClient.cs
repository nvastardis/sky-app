using System;
using System.Diagnostics;
using System.Text.Json;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SkyApp.Data.GeoLocation;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WebClient:
    IWeatherApi
{
    private readonly HttpClient _client;
    private readonly IGeoLocator _geoLocator;
    private readonly string _apiBaseUrl = "https://weatherapi-com.p.rapidapi.com/current.json?";

    public WebClient(IGeoLocator geoLocator)
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

        if (queryParameters.locationStatus != GeoLocatorStatus.Success)
        {
            result.Weather = null;
            switch (queryParameters.locationStatus)
            {
                case GeoLocatorStatus.PermissionException:
                    result.Status = WeatherApiResponseStatus.ErrorFindingLocationPermission;
                    break;
                case GeoLocatorStatus.FeatureNotEnabledException:
                    result.Status = WeatherApiResponseStatus.ErrorFindingLocationFeatureNotEnabled;
                    break;
                case GeoLocatorStatus.FeatureNotSupportedException:
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
        var body = await response.Content.ReadAsStringAsync();
        var bodyStream = new MemoryStream(Encoding.UTF8.GetBytes(body));
        result.Weather = await JsonSerializer.DeserializeAsync<WeatherDto>(bodyStream);
        result.Status = WeatherApiResponseStatus.Success;
        return result;
    }
    
    private async Task<(string locationParameter,  GeoLocatorStatus locationStatus)> SetQueryParametersViaGeoLocator(string location = null)
    {
        var query = "q=";
        if (location is not null)
        {
            return ($"{query}{location}", GeoLocatorStatus.Success);
        }
        
        var currentLocation = await _geoLocator.GetCurrentLocationAsync();
        if (currentLocation.LocationFound is null)
        {
            return (null, currentLocation.ResponseStatus);
        }

        var locationParams =
            $"{currentLocation.LocationFound.Latitude.ToString(CultureInfo.InvariantCulture)},{currentLocation.LocationFound.Longitude.ToString(CultureInfo.InvariantCulture)}";
        
        return ($"{query}{locationParams}", GeoLocatorStatus.Success);
    }
}