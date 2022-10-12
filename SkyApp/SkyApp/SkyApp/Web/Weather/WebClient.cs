using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using SkyApp.Data.GeoLocation;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WebClient :
    IWeatherApi
{
    private readonly IGeoLocator _geoLocator;
    private readonly HttpClient _client;


    public WebClient(IGeoLocator geoLocator, HttpClient client)
    {
        _geoLocator = geoLocator;
        _client = client;
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
                GeoLocatorStatus.PermissionException 
                    => WeatherApiResponseStatus.ErrorFindingLocationPermission,

                GeoLocatorStatus.FeatureNotEnabledException 
                    => WeatherApiResponseStatus.ErrorFindingLocationFeatureNotEnabled,

                GeoLocatorStatus.FeatureNotSupportedException
                    => WeatherApiResponseStatus.ErrorFindingLocationFeatureNotSupported,

                _ 
                    => WeatherApiResponseStatus.Undefined
            };
            return result;
        }

        result.Weather =
            await _client.GetFromJsonAsync<WeatherDto>($"current.json?{queryParameters.locationParameter}", cts);
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