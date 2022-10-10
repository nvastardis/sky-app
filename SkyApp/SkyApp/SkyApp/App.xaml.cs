using System;
using Microsoft.Extensions.DependencyInjection;
using SkyApp.Data.LocationFinder;
using SkyApp.Web.Weather;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using WeatherWebClient = SkyApp.Web.Weather.WebClient;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SkyApp;

public partial class App : Application
{
    private static IServiceProvider ServiceProvider;
    public App()
    {
        SetupServices();
        InitializeComponent();
        MainPage = new MainPage(ServiceProvider);
    }

    protected override void OnStart()
    {
        // Handle when your app starts
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
        var services = new ServiceCollection();

        services.AddSingleton<IWeatherApi, WeatherWebClient>();
        services.AddSingleton<ILocationFinder, LocationFinder>();
            
        ServiceProvider = services.BuildServiceProvider();
    }
}