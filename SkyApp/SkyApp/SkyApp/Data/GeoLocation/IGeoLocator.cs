using System.Threading.Tasks;

namespace SkyApp.Data.GeoLocation;

public interface IGeoLocator
{
    Task<GeoLocatorResponse> GetCurrentLocationAsync();
}