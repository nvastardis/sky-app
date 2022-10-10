using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials; 

namespace SkyApp.Data.LocationFinder;

public class LocationFinder 
    : ILocationFinder
{
    public async Task<LocationFinderResponse> GetCurrentLocationAsync()
    {
        LocationFinderResponse response = new();
        CancellationTokenSource cts = new();
        try
        {
            GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            response.LocationFound = await Geolocation.GetLocationAsync(request, cts.Token);
            response.ResponseStatus = LocationFinderStatus.Success;
        }
        catch (FeatureNotSupportedException)
        {
            response.LocationFound = null;
            response.ResponseStatus = LocationFinderStatus.FeatureNotSupportedException;
        }
        catch (FeatureNotEnabledException)
        {
            response.LocationFound = null;
            response.ResponseStatus = LocationFinderStatus.FeatureNotEnabledException;
        }
        catch (PermissionException)
        {
            response.LocationFound = null;
            response.ResponseStatus = LocationFinderStatus.PermissionException;
        }
        catch (Exception)
        {
            response.LocationFound = null;
            response.ResponseStatus = LocationFinderStatus.Undefined;
        }

        return response;
    }
}