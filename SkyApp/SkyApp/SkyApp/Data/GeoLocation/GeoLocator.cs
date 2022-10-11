using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkyApp.Data.GeoLocation;

public class GeoLocator
    : IGeoLocator
{
    public async Task<GeoLocatorResponse> GetCurrentLocationAsync()
    {
        GeoLocatorResponse response = new();
        CancellationTokenSource cts = new();
        try
        {
            GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            response.LocationFound = await Geolocation.GetLocationAsync(request, cts.Token);
            response.ResponseStatus = GeoLocatorStatus.Success;
        }
        catch (FeatureNotSupportedException)
        {
            response.LocationFound = null;
            response.ResponseStatus = GeoLocatorStatus.FeatureNotSupportedException;
        }
        catch (FeatureNotEnabledException)
        {
            response.LocationFound = null;
            response.ResponseStatus = GeoLocatorStatus.FeatureNotEnabledException;
        }
        catch (PermissionException)
        {
            response.LocationFound = null;
            response.ResponseStatus = GeoLocatorStatus.PermissionException;
        }
        catch (Exception)
        {
            response.LocationFound = null;
            response.ResponseStatus = GeoLocatorStatus.Undefined;
        }

        return response;
    }
}