using System;
using SkyApp.Data;
using SkyApp.Pages;
using Xamarin.Forms;

namespace SkyApp;
public partial class MainPage : FlyoutPage
{
    private readonly FlyOutMenuPage _flyoutMenuPage;
    
    public MainPage()
    {
        _flyoutMenuPage = new();
        Flyout = _flyoutMenuPage;
        Detail = new NavigationPage(new());
        InitializeComponent();
    }
    
    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not FlyOutPageItem item) return;
        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        _flyoutMenuPage.ListView.SelectedItem = null;
        IsPresented = false;
    }
}