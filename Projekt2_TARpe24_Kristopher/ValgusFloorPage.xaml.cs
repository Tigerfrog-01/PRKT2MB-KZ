using Microsoft.Maui.Controls.Shapes;



namespace Projekt2_TARpe24_Kristopher;

public partial class ValgusFloorPage : ContentPage
{
    VerticalStackLayout vsl;
    Ellipse RedPall, YellowPall, GreenPall;
    Label statusLabel;
    Label statusLabelNight;
    BoxView ValgusFloor;
    bool isSystemOn = false;
    bool isSystemOnNight = false;
    HorizontalStackLayout hsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };

    public ValgusFloorPage()
    {
        InitializeComponent();


        //valgusfloori keha
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


        //GRID süsteem
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


        //SEADISTA ALGSED VÄRVID
        RedPall = CreatePall(Color.FromHex("#440000"));
        YellowPall = CreatePall(Color.FromHex("#444400"));
        GreenPall = CreatePall(Color.FromHex("#004400"));
        statusLabel = CreateLabel("Vali valgus");


        //KUTSU VÄLJA FUNKTSIOON "AddTapGesture"
        AddTapGesture(RedPall, "Seisa", Colors.Red, 0);
        AddTapGesture(YellowPall, "Oota", Colors.Yellow, 1);
        AddTapGesture(GreenPall, "Sõida", Colors.Lime, 2);

        


        //GRID JÄRJEKORRA PANEMINE
        Grid.SetRowSpan(ValgusFloor, 3);
        Grid.SetRow(RedPall, 0);
        Grid.SetRow(statusLabel, 0);
        Grid.SetRow(YellowPall, 1);
        Grid.SetRow(GreenPall, 2);

        //OBJEJKI JOONISTAMINE
        ellipsiKonteiner.Children.Add(ValgusFloor);
        ellipsiKonteiner.Children.Add(RedPall);
        ellipsiKonteiner.Children.Add(YellowPall);
        ellipsiKonteiner.Children.Add(GreenPall);
        ellipsiKonteiner.Children.Add(statusLabel);

        //ALGNE NUPP
        Button toggleButton = new Button
        {
            Text = "Lülita sisse",
            Margin = new Thickness(0, 20),
            HorizontalOptions = LayoutOptions.Center
        };
        //NUPP KUI KLIKITUD
        toggleButton.Clicked += (s, e) =>
        {
            isSystemOn = !isSystemOn;
            toggleButton.Text = isSystemOn ? "Lülita välja" : "Lülita sisse";
            if (!isSystemOn) ResetLights();
        };

        //ALGNE NUPP ÖÖ
        Button toggleButtonNight = new Button
        {
            Text = "Lülita öö sisse",
            Margin = new Thickness(0, 20),
            HorizontalOptions = LayoutOptions.Center
        };
        //NUPP KUI KLIKITUD ÖÖ
        toggleButtonNight.Clicked += (s, e) =>
        {
            isSystemOnNight = !isSystemOnNight;
            toggleButtonNight.Text = isSystemOnNight ? "Lülita öövälja" : "Lülita öö sisse";
            if (!isSystemOnNight) ResetLights();
        };



       

        //OBJEKTI VÄLJA KUTSUMINE
        vsl = new VerticalStackLayout
        {
            VerticalOptions = LayoutOptions.Center,
            Children = { ellipsiKonteiner, toggleButton, hsl, toggleButtonNight }
            
        };

        this.Content = vsl;
    }

    //KLIKIMISE FUNKTSIOON
    private void AddTapGesture(Ellipse pall, string msg, Color activeColor, int row)
    {
        var tap = new TapGestureRecognizer();
        tap.Tapped += async(s, e) =>
        {

            //KONTROLL KAS KÕIK ON VÄLJAS , KUI ON SIIS ERROR
            if (!isSystemOn && !isSystemOnNight)
            {
                statusLabel.Text = "Lülita esmalt floor sisse";
                Grid.SetRow(statusLabel, row);
                return;
            }

            //KONTROLLIB KAS ÖÖ ON SEES
            if (isSystemOnNight)
            {
                BackgroundImageSource = "https://media.istockphoto.com/id/523538287/photo/times-square.jpg?s=612x612&w=0&k=20&c=gZMU_YAcKxkwUCurZkkAjYOdZfnxhcA_sZnpHMx703A=";
                if (pall == YellowPall)
                {
                    ResetLights();
                    pall.Fill = Colors.Yellow;
                    statusLabel.Text = "Sõida või muidu..";

                    
                    
                        await pall.FadeTo(0, 500);
                        await pall.FadeTo(1, 500);
                    

                }
                else
                {
                    statusLabel.Text = "Praegu on öö, klikki kollast";
                }

                Grid.SetRow(statusLabel, row);
                return; 
            }

            //KONTROLLIB KAS PÄEV ON SEES
            if (isSystemOn)
            {
                BackgroundImageSource = "https://media.istockphoto.com/id/2156308388/photo/crosswalk-new-york-street-scene-usa-stock-photo.jpg?s=612x612&w=0&k=20&c=ObWAbO24AfTN_oSiURbaEypaA0HBkzIEBgP4ew9l-ck=";
                ResetLights();
                pall.Fill = activeColor;
                statusLabel.Text = msg; 
                Grid.SetRow(statusLabel, row);
            }



            ResetLights();
            pall.Opacity = 1;
            pall.Fill = activeColor;
            statusLabel.Text = msg;
            Grid.SetRow(statusLabel, row);
        };
        pall.GestureRecognizers.Add(tap);
    }


    //TOOB VALGUSE ALGSE VERSIOONI TAGASI
    private void ResetLights()
    {
        RedPall.Fill = Color.FromHex("#440000");
        YellowPall.Fill = Color.FromHex("#444400");
        GreenPall.Fill = Color.FromHex("#004400");
        statusLabel.Text = isSystemOn ? "Vali valgus" : "";
    }

    //JOONISTAB PALLI
    private Ellipse CreatePall(Color varv) => new Ellipse
    {
        WidthRequest = 150,
        HeightRequest = 150,
        Fill = varv,
        Stroke = Colors.Black,
        StrokeThickness = 2,
        HorizontalOptions = LayoutOptions.Center
    };


    //JOONISTAB TEKSTI
    private Label CreateLabel(string tekst) => new Label
    {
        Text = tekst,
        HorizontalOptions = LayoutOptions.Center,
        VerticalOptions = LayoutOptions.Center,
        TextColor = Colors.Black,
        FontAttributes = FontAttributes.Bold,
        InputTransparent = true
    };


    //NAV FUNKTSIOON
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