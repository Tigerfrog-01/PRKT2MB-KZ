namespace Projekt2_TARpe24_Kristopher;

public partial class TripsPea : ContentPage
{

    Color currentBg = Color.FromHex("#D2923E"); 
    Color currentText = Colors.White;

    public Dictionary<string, List<(string Name, ContentPage Page)>> Categories = new()
    {
        { "Peamenüü", new List<(string, ContentPage)> {
            ("Alusta", new TripsTraps()),
        }},
    };

    public TripsPea()
    {
        InitializeComponent();

        foreach (var category in Categories)
        {
            foreach (var item in category.Value)
            {
                Button navButton = new Button
                {
                    Text = item.Name,
                    FontSize = 22,
                    BackgroundColor = Colors.DarkSlateBlue,
                    TextColor = Colors.White,
                    HeightRequest = 60,
                    WidthRequest = 250,
                    CornerRadius = 10,
                    Margin = new Thickness(0, 0, 0, 10)
                };

          
                navButton.Clicked += async (s, e) =>
                {
                    var gamePage = new TripsTraps();
                 
                    gamePage.ApplyTheme(currentBg, currentText);              
                    await Navigation.PushAsync(gamePage);
                };

                MainStack.Children.Insert(0, navButton);
            }
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Vali teema", "Loobu", null, "Hele", "Tume", "Kalapulk");

        if (action == "Hele") { currentBg = Colors.White; currentText = Colors.Black; }
        else if (action == "Tume") { currentBg = Color.FromHex("#2C4251"); currentText = Colors.White; }
        else if (action == "Kalapulk") { currentBg = Color.FromHex("#D2923E"); currentText = Colors.White; }

   
        this.BackgroundColor = currentBg;
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Edetabel", "1. Kristopher - 50 võitu\n2. Mängija 2 - 30 võitu", "Sulge");
    }
}