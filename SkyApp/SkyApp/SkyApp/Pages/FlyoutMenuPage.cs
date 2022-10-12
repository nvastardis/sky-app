using System.Collections.Generic;
using SkyApp.Data;
using Xamarin.Forms;

namespace SkyApp.Pages;

public class FlyOutMenuPage : ContentPage
{
    public ListView ItemList { get; }

    public FlyOutMenuPage()
    {
        var flyoutPageItems = new List<FlyOutPageItem>
        {
            new()
            {
                Title = "Weather",
                IconSource = "",
                TargetType = typeof(WeatherPage)
            },
            new()
            {
                Title = "Night Sky",
                IconSource = "",
                TargetType = typeof(NightSkyPage)
            },
            new()
            {
                Title = "Moon",
                IconSource = "",
                TargetType = typeof(MoonPage)
            }
        };

        ItemList = new()
        {
            ItemsSource = flyoutPageItems,
            ItemTemplate = new(() =>
            {
                Grid grid = new() { Padding = new Thickness(5, 10) };
                grid.ColumnDefinitions.Add(new() { Width = new GridLength(30) });
                grid.ColumnDefinitions.Add(new() { Width = GridLength.Star });

                Label label = new() { VerticalOptions = LayoutOptions.FillAndExpand };
                label.SetBinding(Label.TextProperty, "Title");

                grid.Children.Add(label, 1, 0);

                ViewCell cell = new()
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
            Children = { ItemList }
        };
    }
}