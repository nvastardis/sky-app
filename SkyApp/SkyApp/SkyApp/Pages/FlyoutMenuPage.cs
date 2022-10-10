using System.Collections.Generic;
using SkyApp.Data;
using Xamarin.Forms;

namespace SkyApp.Pages;

public class FlyOutMenuPage : ContentPage
{

    private readonly ListView listView;
    public ListView ItemList
    {
        get { return listView; }
    }

    public FlyOutMenuPage()
    {
        var flyoutPageItems = new List<FlyOutPageItem>();
        flyoutPageItems.Add(new ()
        {
            Title = "Weather",
            IconSource = "",
            TargetType = typeof(WeatherPage)
        });
        flyoutPageItems.Add(new()
        {
            Title = "Night Sky",
            IconSource = "",
            TargetType = typeof(NightSkyPage)
        });
        flyoutPageItems.Add(new ()
        {
            Title = "Moon",
            IconSource = "",
            TargetType = typeof(MoonPage)
        });

        listView = new()
        {
            ItemsSource = flyoutPageItems,
            ItemTemplate = new (() =>
            {
                Grid grid = new() { Padding = new Thickness(5, 10) };
                grid.ColumnDefinitions.Add(new() { Width = new GridLength(30) });
                grid.ColumnDefinitions.Add(new() { Width = GridLength.Star });
                
                Label label = new() { VerticalOptions = LayoutOptions.FillAndExpand };
                label.SetBinding(Label.TextProperty, "Title");
                
                grid.Children.Add(label, 1, 0);

                ViewCell cell = new ()
                {
                    View = grid
                };
                
                return cell;
            }),
            SeparatorVisibility = SeparatorVisibility.None
        };

        IconImageSource = "hamburger.png";
        Title = "Personal Organiser";
        Padding = new(0, 40, 0, 0);
        Content = new StackLayout
        {
            Children = { listView }
        };
    }
}