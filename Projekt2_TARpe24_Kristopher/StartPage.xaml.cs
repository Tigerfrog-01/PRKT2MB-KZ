namespace Projekt2_TARpe24_Kristopher;

public partial class StartPage : ContentPage
{
    VerticalStackLayout vst;
    ScrollView sv;

  
    public Dictionary<string, List<(string Name, ContentPage Page)>> Categories = new()
    {
        { "Baaslehed", new List<(string, ContentPage)> {
            ("Tekst", new TextPage()),
            ("Kujund", new FigurePage()),
            ("Popup", new Popup())
        }},
        { "Funktsioonid", new List<(string, ContentPage)> {
            ("Timer", new TimerPage()),
            ("DateTime", new DateTimePage(0))
        }},
        { "Interaktiivne", new List<(string, ContentPage)> {
            ("Valgusfoor", new ValgusFloorPage()),
            ("Stepper/Slide", new StepperSlidePage()),
            ("RGB", new RGB()),
            ("Lumememm", new Lumememm()),
             ("PopUpProject", new PopUpProject()),
            ("PickerImagePage", new PickerImagePage()),
                ("TripsTraps", new TripsTraps())
        }}
    };

    public StartPage()
    {
        InitializeComponent();

        vst = new VerticalStackLayout { Padding = 20, Spacing = 10 };

       
        foreach (var category in Categories)
        {
            Button catButton = new Button
            {
                Text = $"📁 {category.Key}",
                BackgroundColor = Colors.DarkSlateBlue,
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = 24
            };

            VerticalStackLayout itemContainer = new VerticalStackLayout
            {
                IsVisible = false,
                Margin = new Thickness(20, 0, 0, 0),
                Spacing = 5
            };

            catButton.Clicked += (s, e) => itemContainer.IsVisible = !itemContainer.IsVisible;

            foreach (var item in category.Value)
            {
                Button navButton = new Button
                {
                    Text = item.Name,
                    FontSize = 20,
                    BackgroundColor = Colors.LightGray,
                    TextColor = Colors.Black,
                    HeightRequest = 50
                };

                navButton.Clicked += async (s, e) => await Navigation.PushAsync(item.Page);
                itemContainer.Add(navButton);
            }

            vst.Add(catButton);
            vst.Add(itemContainer);
        }

       
        Button resetBtn = new Button
        {
            Text = "Nulli seaded (Testimiseks)",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 50,
            Margin = new Thickness(0, 30, 0, 0)
        };

        resetBtn.Clicked += NulliNupp_Clicked;
        vst.Add(resetBtn);

        sv = new ScrollView { Content = vst };
        Content = sv;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        bool onEsimeneStart = Preferences.Default.Get("EsimeneKäivitamine", true);

        if (onEsimeneStart)
        {
            bool vastus = await DisplayAlert("Tere tulemast!",
                "Tundub, et avasid selle rakenduse esimest korda. Kas soovid näha lühikest juhendit?",
                "Jah, palun", "Ei, saan ise hakkama");

            if (vastus)
            {
                await DisplayAlert("Juhend",
                    "Siin on sinu lühike juhend: ava kaustad ja vali sobiv teema!",
                    "Selge");
            }

            Preferences.Default.Set("EsimeneKäivitamine", false);
        }
    }

    private async void NulliNupp_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Remove("EsimeneKäivitamine");
        await DisplayAlert("Edukalt nullitud", "Seaded on nullitud. Restartides näed tervitust uuesti.", "OK");
    }
}