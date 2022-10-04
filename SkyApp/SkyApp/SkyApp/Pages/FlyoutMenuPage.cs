using System;
using System.Collections.Generic;
using SkyApp.Data;
using Xamarin.Forms;

namespace SkyApp.Pages;

public class FlyOutMenuPage : ContentPage
{

    private readonly ListView listView;
    public ListView ListView => listView;

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
            IconSource = "todo.png",
            TargetType = typeof(NightSkyPage)
        });
        flyoutPageItems.Add(new ()
        {
            Title = "Moon",
            IconSource = "reminders.png",
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

                // var image = new Image();
                // image.SetBinding(Image.SourceProperty, "IconSource");
                Label label = new() { VerticalOptions = LayoutOptions.FillAndExpand };
                label.SetBinding(Label.TextProperty, "Title");

                // grid.Children.Add(image);
                grid.Children.Add(label, 1, 0);

                return new ViewCell { View = grid };
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