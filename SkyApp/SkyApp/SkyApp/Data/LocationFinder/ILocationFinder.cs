using System.Threading.Tasks;

namespace SkyApp.Data.LocationFinder;

public interface ILocationFinder
{
    Task<LocationFinderResponse> GetCurrentLocationAsync();
}