namespace SkyApp.Web.Weather;

public enum WeatherApiResponseStatus
{
    Undefined = 0,
    ErrorFindingLocationFeatureNotSupported = 1,
    ErrorFindingLocationFeatureNotEnabled = 2,
    ErrorFindingLocationPermission = 3,
    Success = 4
}