using System;
using System.Diagnostics;
using System.Text.Json;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
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
    
    
    public async Task<WeatherApiResponse> GetWeather(string location = null, CancellationToken cts = default)
    {
        
        var queryParameters = await SetQueryParametersViaGeoLocator(location, cts);
        WeatherApiResponse result = new();

        if (queryParameters.locationStatus != GeoLocatorStatus.Success)
        {
            result.Weather = null;
            result.Status = queryParameters.locationStatus switch
            {
                GeoLocatorStatus.PermissionException => WeatherApiResponseStatus.ErrorFindingLocationPermission,
                GeoLocatorStatus.FeatureNotEnabledException => WeatherApiResponseStatus.ErrorFindingLocationFeatureNotEnabled,
                GeoLocatorStatus.FeatureNotSupportedException => WeatherApiResponseStatus.ErrorFindingLocationFeatureNotSupported,
                _ => WeatherApiResponseStatus.Undefined
            };

            return result;

        }
        
        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new($"{_apiBaseUrl}{queryParameters.locationParameter}")
        };
        using var response = await _client.SendAsync(request, cts);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStreamAsync();
        
        result.Weather = await JsonSerializer.DeserializeAsync<WeatherDto>(body, JsonSerializerOptions.Default, cts);
        result.Status = WeatherApiResponseStatus.Success;
        return result;
    }
    
    private async Task<(string locationParameter, GeoLocatorStatus locationStatus)> SetQueryParametersViaGeoLocator(
        string location = null, CancellationToken cts = default)
    {
        const string query = "q=";
        if (location is not null)
        {
            return ($"{query}{location}", GeoLocatorStatus.Success);
        }
        
        var currentLocation = await _geoLocator.GetCurrentLocationAsync(cts);
        if (currentLocation.LocationFound is null)
        {
            return (null, currentLocation.ResponseStatus);
        }

        var locationParams =
            $"{currentLocation.LocationFound.Latitude.ToString(CultureInfo.InvariantCulture)},{currentLocation.LocationFound.Longitude.ToString(CultureInfo.InvariantCulture)}";
        
        return ($"{query}{locationParams}", GeoLocatorStatus.Success);
    }
}