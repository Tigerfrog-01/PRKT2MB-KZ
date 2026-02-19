using Microsoft.Maui.Layouts;

namespace Projekt2_TARpe24_Kristopher;

public partial class StepperSlidePage : ContentPage
{
    Label label;
    Stepper stepper;
    Slider slider;
    AbsoluteLayout al;
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public StepperSlidePage()
    {
        InitializeComponent();

        //NAV nuppud
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

        label = new Label
        {
            Text = "...",
            BackgroundColor = Colors.LightGray,
        };

        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 360,
            Increment = 5,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += Stepper_Slider_ValueChanged;

        slider = new Slider
        {
            Minimum = 0,
            Maximum = 360,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300,
            ThumbImageSource = "../Resources/Images/thumb.png"
        };
        slider.ValueChanged += Stepper_Slider_ValueChanged;

        al = new AbsoluteLayout { Children = { label, stepper, slider,hsl } };

        List<View> controls = new List<View> { label, stepper, slider };

        for (int i = 0; i < controls.Count; i++)
        {
            double yKoht = 0.2 + i * 0.2;
            AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 300, 60));
            AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);
        }

        Content = al;
    }

    private void Stepper_Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        label.Text = $"Stepperi/Slideri väärtus: {e.NewValue:F0}";
        label.FontSize = 24 + e.NewValue / 4;
        label.BackgroundColor = Color.FromRgb((int)(e.NewValue * 2.55), (int)(255 - e.NewValue * 2.55), 128);
        label.TextColor = Color.FromRgb((int)(255 - e.NewValue * 2.55), (int)(e.NewValue * 2.55), 128);
        label.Rotation = e.NewValue;
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
