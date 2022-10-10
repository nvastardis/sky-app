namespace SkyApp.Data.LocationFinder;

public enum LocationFinderStatus
{
    Undefined = 0,
    FeatureNotSupportedException = 1,
    FeatureNotEnabledException = 2,
    PermissionException = 3,
    Success = 4
}