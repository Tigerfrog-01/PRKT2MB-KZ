using Microsoft.Maui.Controls.Shapes;



namespace Projekt2_TARpe24_Kristopher;

public partial class ValgusFloorPage : ContentPage
{
    VerticalStackLayout vsl;
    Ellipse RedPall, YellowPall, GreenPall;
    Label statusLabel;
    BoxView ValgusFloor;
    bool isSystemOn = false;
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };

    public ValgusFloorPage()
    {
        InitializeComponent();

        ValgusFloor = new BoxView
        {
            Color = Colors.Gray,
            Background = Colors.Transparent,
            WidthRequest = 200,
            HeightRequest = 500,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            CornerRadius = 30,
        };

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

        Grid ellipsiKonteiner = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            RowSpacing = 10,
            RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            }
        };

        RedPall = CreatePall(Color.FromHex("#440000"));
        YellowPall = CreatePall(Color.FromHex("#444400"));
        GreenPall = CreatePall(Color.FromHex("#004400"));
        statusLabel = CreateLabel("Vali valgus");

        AddTapGesture(RedPall, "Seisa", Colors.Red, 0);
        AddTapGesture(YellowPall, "Oota", Colors.Yellow, 1);
        AddTapGesture(GreenPall, "Sõida", Colors.Lime, 2);

        Grid.SetRowSpan(ValgusFloor, 3);
        Grid.SetRow(RedPall, 0);
        Grid.SetRow(statusLabel, 0);
        Grid.SetRow(YellowPall, 1);
        Grid.SetRow(GreenPall, 2);

        ellipsiKonteiner.Children.Add(ValgusFloor);
        ellipsiKonteiner.Children.Add(RedPall);
        ellipsiKonteiner.Children.Add(YellowPall);
        ellipsiKonteiner.Children.Add(GreenPall);
        ellipsiKonteiner.Children.Add(statusLabel);

        Button toggleButton = new Button
        {
            Text = "Lülita sisse",
            Margin = new Thickness(0, 20),
            HorizontalOptions = LayoutOptions.Center
        };
        toggleButton.Clicked += (s, e) =>
        {
            isSystemOn = !isSystemOn;
            toggleButton.Text = isSystemOn ? "Lülita välja" : "Lülita sisse";
            if (!isSystemOn) ResetLights();
        };

        vsl = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            Children = { ellipsiKonteiner, toggleButton, hsl }
            
        };

        this.Content = vsl;
    }

    private void AddTapGesture(Ellipse pall, string msg, Color activeColor, int row)
    {
        var tap = new TapGestureRecognizer();
        tap.Tapped += (s, e) =>
        {
            if (!isSystemOn)
            {
                statusLabel.Text = "Lülita esmalt foor sisse";
                Grid.SetRow(statusLabel, row);
                return;
            }

            ResetLights();
            pall.Fill = activeColor;
            statusLabel.Text = msg;
            Grid.SetRow(statusLabel, row);
        };
        pall.GestureRecognizers.Add(tap);
    }

    private void ResetLights()
    {
        RedPall.Fill = Color.FromHex("#440000");
        YellowPall.Fill = Color.FromHex("#444400");
        GreenPall.Fill = Color.FromHex("#004400");
        statusLabel.Text = isSystemOn ? "Vali valgus" : "";
    }

    private Ellipse CreatePall(Color varv) => new Ellipse
    {
        WidthRequest = 150,
        HeightRequest = 150,
        Fill = varv,
        Stroke = Colors.Black,
        StrokeThickness = 2,
        HorizontalOptions = LayoutOptions.Center
    };

    private Label CreateLabel(string tekst) => new Label
    {
        Text = tekst,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center,
        TextColor = Colors.Black,
        FontAttributes = FontAttributes.Bold,
        InputTransparent = true
    };

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