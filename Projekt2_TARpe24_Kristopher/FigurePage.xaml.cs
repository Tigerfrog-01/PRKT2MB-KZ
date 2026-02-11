using Microsoft.Maui.Controls.Shapes;

namespace Projekt2_TARpe24_Kristopher;

public partial class FigurePage : ContentPage
{
    BoxView boxView;
    Ellipse pall;
    Polygon kolmnurk;
    Random rnd = new Random();
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    VerticalStackLayout vsl;

    public FigurePage()
    {
        // Random initial colors
        int r = rnd.Next(256);
        int g = rnd.Next(256);
        int b = rnd.Next(256);

        // BoxView setup
        boxView = new BoxView
        {
            Color = Color.FromRgb(r, g, b),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0), // transparent
            CornerRadius = 30,
        };

        TapGestureRecognizer tap = new TapGestureRecognizer();
        boxView.GestureRecognizers.Add(tap);
        tap.Tapped += (sender, e) =>
        {
            int r2 = rnd.Next(256);
            int g2 = rnd.Next(256);
            int b2 = rnd.Next(256);
            boxView.Color = Color.FromRgb(r2, g2, b2);
            boxView.WidthRequest = boxView.Width + 20;
            boxView.HeightRequest = boxView.Height + 30;

            if (boxView.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width / 3)
            {
                boxView.WidthRequest = 200;
                boxView.HeightRequest = 200;
            }
        };

        pall = new Ellipse
        {
            WidthRequest = 200,
            HeightRequest = 200,
            Fill = new SolidColorBrush(Color.FromRgb(b, g, r)), // Shape color via brush
            Stroke = Colors.BurlyWood, // Border color
            StrokeThickness = 5, // Border thickness
            HorizontalOptions = LayoutOptions.Center
        };
        pall.GestureRecognizers.Add(tap);

        // Polygon setup
        kolmnurk = new Polygon
        {
            Points = new PointCollection
    {
        new Point(0, 200),   // Bottom left
        new Point(100, 0),   // Center top
        new Point(200, 200)  // Bottom right
    },
            Fill = new SolidColorBrush(Color.FromRgb(g, b, r)), // Shape color via brush
            Stroke = Colors.Aquamarine, // Border color
            StrokeThickness = 5, // Border thickness
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        TapGestureRecognizer tap_kolmnurk = new TapGestureRecognizer();
        tap_kolmnurk.NumberOfTapsRequired = 2; // Double tap
        kolmnurk.GestureRecognizers.Add(tap_kolmnurk);
        tap_kolmnurk.Tapped += (sender, e) =>
        {
            // add your own logic here
        };

        // Navigation Buttons
        hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 28,
                FontFamily = "Luffio",
                TextColor = Colors.Chocolate,
                BackgroundColor = Colors.Beige,
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { boxView, pall, kolmnurk, hsl },
            HorizontalOptions = LayoutOptions.Center
        };

        Content = vsl;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new StartPage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PushAsync(new StartPage());
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new TimerPage()); 
        }
      
    }
}