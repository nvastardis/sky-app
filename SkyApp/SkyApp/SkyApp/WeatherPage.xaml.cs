using SkyApp.Data.Weather;
using SkyApp.Web.Weather;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkyApp;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class WeatherPage : ContentPage
{
    public static BindableProperty WeatherInfo { get; set; } = BindableProperty.Create(
        nameof(Info),
    typeof(WeatherDto),
    typeof(WeatherDto),
    new WeatherDto(),
        BindingMode.OneWay,
        propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var view = bindableObject as WeatherPage; 
            view!.Info = (WeatherDto)newValue;
        });

    public WeatherDto Info
    {
        get => (WeatherDto)GetValue(WeatherInfo);
        set => SetValue(WeatherInfo, value);
    }

    private readonly IWeatherApi _weatherApi;
    public WeatherPage(IWeatherApi api)
    {
        _weatherApi = api;
        InitializeComponent();
        GatherWeatherInfo();
    }

    private async void GatherWeatherInfo()
    {
        
        var response = await _weatherApi.GetWeather();
        switch (response.Status)
        {
            case WeatherApiResponseStatus.Success:
                /*await DisplayAlert("Success!", "Successful fetch for current Location", "OK");*/ 
                Info = response.Weather;
                break;
            case WeatherApiResponseStatus.ErrorFindingLocationPermission:
                await DisplayAlert("Exception!", "Exception: No Permission for Location", "OK");
                break;
            case WeatherApiResponseStatus.ErrorFindingLocationFeatureNotEnabled:
                await DisplayAlert("Exception!", "Exception: Gps Not Enabled", "OK");
                break;
            case WeatherApiResponseStatus.ErrorFindingLocationFeatureNotSupported:
                await DisplayAlert("Exception!", "Exception: Location Finding Not Supported", "OK");
                break;
            default:
                await DisplayAlert("Exception!", "Exception: Undefined Exception was thrown", "OK");
                break;
        }

        DefaultActivityIndicator.IsRunning = false;
        DetailsGrid.IsVisible = true;
    }
}