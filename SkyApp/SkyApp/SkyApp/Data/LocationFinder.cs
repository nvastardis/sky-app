using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SkyApp.Data;

public class LocationFinder
{
    CancellationTokenSource _cts;

    public async Task<Location> GetCurrentLocationAsync()
    {
        try
        {
            GeolocationRequest request = new(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            _cts = new();
            var location = await Geolocation.GetLocationAsync(request, _cts.Token);
            return location;
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Handle not supported on device exception
            return null;
        }
        catch (FeatureNotEnabledException fneEx)
        {
            // Handle not enabled on device exception
            return null;
        }
        catch (PermissionException pEx)
        {
            // Handle permission exception
            return null;
        }
        catch (Exception ex)
        {
            // Unable to get location
            return null;
        }
    }
}