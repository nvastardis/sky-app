using System;
using Microsoft.Extensions.DependencyInjection;
using SkyApp.Data.GeoLocation;
using SkyApp.Web.Weather;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeatherWebClient = SkyApp.Web.Weather.WebClient;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SkyApp;

public partial class App : Application
{
    private static IServiceProvider _serviceProvider;

    public App()
    {
        SetupServices();
        InitializeComponent();
        MainPage = new MainPage(_serviceProvider);
    }

    protected override void OnStart()
    {
        Permissions.RequestAsync<Permissions.LocationWhenInUse>();
    }

    protected override void OnSleep()
    {
        // Handle when your app sleeps
    }

    protected override void OnResume()
    {
        // Handle when your app resumes
    }

    private void SetupServices()
    {
        ServiceCollection services = new();

        services.AddSingleton<IWeatherApi, WeatherWebClient>();
        services.AddSingleton<IGeoLocator, GeoLocator>();
        services.AddHttpClient<IWeatherApi, WeatherWebClient>(client =>
        {
            client.BaseAddress = new("https://weatherapi-com.p.rapidapi.com/");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Test");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
        });
        _serviceProvider = services.BuildServiceProvider();
    }
}