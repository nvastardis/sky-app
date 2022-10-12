using System;
using Microsoft.Extensions.DependencyInjection;
using SkyApp.Data;
using SkyApp.Pages;
using Xamarin.Forms;

namespace SkyApp;

public partial class MainPage : FlyoutPage
{
    private readonly FlyOutMenuPage _flyoutMenuPage;
    private readonly IServiceProvider _provider;

    public MainPage(IServiceProvider serviceProvider)
    {
        _provider = serviceProvider;
        _flyoutMenuPage = new();
        _flyoutMenuPage.ItemList.ItemSelected += OnItemSelected;
        Flyout = _flyoutMenuPage;
        Detail = new NavigationPage(new());
        InitializeComponent();
    }

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not FlyOutPageItem item) return;
        Detail = new NavigationPage((Page)ActivatorUtilities.CreateInstance(_provider, item.TargetType));
        _flyoutMenuPage.ItemList.SelectedItem = null;
        IsPresented = false;
    }
}