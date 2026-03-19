using Microsoft.Maui.Graphics.Text;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace Projekt2_TARpe24_Kristopher;

public partial class TripsPea : ContentPage
{
    

    Color currentBg = Color.FromHex("#D2923E"); 
    Color currentText = Colors.White;

    string name;

    Label titleLabel;


  


    
    public Dictionary<string, List<(string Name, ContentPage Page)>> Categories = new()
    {
        { "Peamenüü", new List<(string, ContentPage)> {
            ("Alusta", new TripsTraps()),
        }},
    };



   


    public TripsPea()
    {
        InitializeComponent();

      
        name = Preferences.Default.Get("SavedPlayerName", "Külaline");

        Button Nimi = new Button
        {
          
            Text = name == "Külaline" ? "Logi sisse" : $"Kasutaja: {name}",
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            TextColor = Colors.White,
            BackgroundColor = Colors.DarkSlateBlue,
            Margin = new Thickness(0, 5, 10, 0)
        };

        Nimi.Clicked += NimiButton_Clicked;
        MainStack.Children.Add(Nimi);

        titleLabel = new Label
        {
            Text = "Trips-Traps-Trull",
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 20)
        };
        MainStack.Children.Add(titleLabel);

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
                 
                    string mode = await DisplayActionSheet("Vali mängu tüüp", "Loobu", null, "1vs1", "Robot");
                    if (mode == "Loobu" || mode == null) return;

                    string p1 = name; 
                    string p2 = "Robot";

                    if (mode == "1vs1")
                    {
                       
                        string side = await DisplayActionSheet($"{p1}, vali oma sümbol", "Loobu", null, "X (Punane)", "O (Sinine)");
                        if (side == "Loobu" || side == null) return;

                      
                        p2 = await DisplayPromptAsync("Teine mängija", "Sisesta teise mängija nimi:", placeholder: "Mängija 2");
                        if (string.IsNullOrWhiteSpace(p2)) p2 = "Külaline2";

                        AddNameToMasterList(p2);

                        
                        var gamePage = new TripsTraps(p1, p2, side.StartsWith("X"));
                        gamePage.ApplyTheme(currentBg, currentText);
                        await Navigation.PushAsync(gamePage);
                    }
                    else
                    {
                      
                        var gamePage = new TripsTraps(p1, "Robot", true); 
                        gamePage.ApplyTheme(currentBg, currentText);
                        await Navigation.PushAsync(gamePage);
                    }
                };

                MainStack.Children.Add(navButton);
            }
        }

        Button Seaded = new Button
        {
            Text = "Seaded",
            FontSize = 22,
            BackgroundColor = Color.FromHex("#A0522D"),
            TextColor = Colors.White,
            HeightRequest = 60,
            WidthRequest = 250,
            CornerRadius = 10,
            
        };
        Seaded.Clicked += OnSettingsClicked;

        Button Edetabel = new Button
        {
            Text = "Edetabel",
                    FontSize = 22,
                    BackgroundColor = Color.FromHex("#A0522D"),
                    TextColor = Colors.White,
                    HeightRequest = 60,
                    WidthRequest = 250,
                    CornerRadius = 10,
                     };
        Edetabel.Clicked += OnLeaderboardClicked;

        MainStack.Children.Add(Seaded);
        MainStack.Children.Add(Edetabel);
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Vali teema", "Loobu", null, "Hele", "Tume", "Kalapulk");

        if (action == "Hele") { currentBg = Colors.White; currentText = Colors.Black; titleLabel.TextColor = Colors.Black; }
        else if (action == "Tume") { currentBg = Color.FromHex("#2C4251"); currentText = Colors.White; titleLabel.TextColor = Colors.White; }
        else if (action == "Kalapulk") { currentBg = Color.FromHex("#D2923E"); currentText = Colors.White; titleLabel.TextColor = Colors.White; }

   
        this.BackgroundColor = currentBg;
        

    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        string allNamesRaw = Preferences.Default.Get("AllPlayerNames", "");
        if (string.IsNullOrEmpty(allNamesRaw))
        {
            await DisplayAlert("Edetabel", "Mängijaid veel pole!", "Sulge");
            return;
        }

        string[] playerList = allNamesRaw.Split(',');
        string leaderboardText = "EDETABEL:\n\n";

        foreach (string pName in playerList)
        {
           
            int wins = Preferences.Default.Get($"Wins_{pName}", 0);
            leaderboardText += $"{pName}: {wins} võitu\n";
        }

        await DisplayAlert("Edetabel", leaderboardText, "Sulge");
    }

    private async void NimiButton_Clicked(object sender, EventArgs e)
    {
        string input = await DisplayPromptAsync("Logi sisse palun", "Mis su nimi on?", placeholder: "Logi sisse nüüd!");

        if (!string.IsNullOrWhiteSpace(input))
        {
            name = input;
            Preferences.Default.Set("SavedPlayerName", name);

           
            string allNames = Preferences.Default.Get("AllPlayerNames", "");
            if (!allNames.Contains(name))
            {
              
                allNames = string.IsNullOrEmpty(allNames) ? name : $"{allNames},{name}";
                Preferences.Default.Set("AllPlayerNames", allNames);
            }

            ((Button)sender).Text = $"Kasutaja: {name}";
        }

    }
    private void AddNameToMasterList(string newName)
    {
        string allNames = Preferences.Default.Get("AllPlayerNames", "");
        if (!allNames.Contains(newName))
        {
            allNames = string.IsNullOrEmpty(allNames) ? newName : $"{allNames},{newName}";
            Preferences.Default.Set("AllPlayerNames", allNames);
        }
    }
}