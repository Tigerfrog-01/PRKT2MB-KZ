namespace Projekt2_TARpe24_Kristopher;

using System.Text.Json;

public partial class FishMerge : ContentPage
{
    Grid gr4x4;
    Game gameLogic;
    Theme currentTheme;
    string username;
    string filePath = Path.Combine(FileSystem.AppDataDirectory, "leaderboard.json");

    BoxView[,] kasteMaatriks = new BoxView[4, 4];
    Label[,] tekstMaatriks = new Label[4, 4];

    public FishMerge()
    {
        InitializeComponent();
        gameLogic = new Game();
        SetTheme("Kalapulk");
        AskUsername();
    }

    private async void AskUsername()
    {
        while (string.IsNullOrWhiteSpace(username))
        {
            username = await DisplayPromptAsync("Welcome", "Enter your username to play:", "OK", null, "Guest");
        }
        BuildUI();
    }

    private void BuildUI()
    {
        var mainStack = new VerticalStackLayout { Padding = 10, Spacing = 10 };
        var topNav = new Grid
        {
            ColumnDefinitions = {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Auto)
            }
        };

        var scoreLabel = new Label { Text = "SCORE: 0", FontSize = 24, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold };
        this.Resources["ScoreLabel"] = scoreLabel;

        var leaderboardBtn = new Button { Text = "🏆", FontSize = 24, BackgroundColor = Colors.Transparent, TextColor = Colors.Black };
        leaderboardBtn.Clicked += OnLeaderboardClicked;

        var settingsBtn = new Button { Text = "⚙️", FontSize = 24, BackgroundColor = Colors.Transparent, TextColor = Colors.Black };
        settingsBtn.Clicked += OnSettingsClicked;

        topNav.Add(scoreLabel, 0);
        topNav.Add(leaderboardBtn, 1);
        topNav.Add(settingsBtn, 2);

        gr4x4 = Täida_gr4x4();
        mainStack.Add(topNav);
        mainStack.Add(gr4x4);

        Content = mainStack;
        currentTheme.Apply(this);
        UuendaVisuaal();
        LisaSõrmeLiigutused();
    }

    private async void OnLeaderboardClicked(object sender, EventArgs e)
    {
        var scores = LoadScores();
        string display = "TOP SCORES:\n";
        var top10 = scores.OrderByDescending(x => x.Value).Take(10);
        int i = 1;
        foreach (var entry in top10)
        {
            display += $"{i++}. {entry.Key}: {entry.Value}\n";
        }
        await DisplayAlert("LEADERBOARD", display, "OK");
    }

    private void SaveScore(int finalScore)
    {
        var scores = LoadScores();
        string normalizedKey = username.Trim();

        if (!scores.ContainsKey(normalizedKey) || scores[normalizedKey] < finalScore)
        {
            scores[normalizedKey] = finalScore;
            File.WriteAllText(filePath, JsonSerializer.Serialize(scores));
        }
    }

    private Dictionary<string, int> LoadScores()
    {
        if (!File.Exists(filePath)) return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        try
        {
            string json = File.ReadAllText(filePath);
            var data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return new Dictionary<string, int>(data, StringComparer.OrdinalIgnoreCase);
        }
        catch { return new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); }
    }

    private Grid Täida_gr4x4()
    {
        Grid tempGrid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Padding = 10,
            ColumnSpacing = 8,
            RowSpacing = 8,
            WidthRequest = 360,
            HeightRequest = 360
        };

        for (int i = 0; i < 4; i++)
        {
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Grid tileContainer = new Grid();
                BoxView pesa = new BoxView { CornerRadius = 8 };
                BoxView valgeKast = new BoxView { CornerRadius = 8, IsVisible = false };
                Image kalaPilt = new Image { Source = "kalapulk1.png", Aspect = Aspect.AspectFit, IsVisible = false, Scale = 0.8 };
                Label tekst = new Label { Text = "", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, FontSize = 28 };

                kasteMaatriks[r, c] = valgeKast;
                tekstMaatriks[r, c] = tekst;

                tileContainer.Children.Add(pesa);
                tileContainer.Children.Add(valgeKast);
                tileContainer.Children.Add(kalaPilt);
                tileContainer.Children.Add(tekst);
                tempGrid.Add(tileContainer, c, r);
            }
        }
        return tempGrid;
    }

    public void UuendaVisuaal()
    {
        gr4x4.BackgroundColor = currentTheme.Name == "Neon" ? Colors.Black :
                               currentTheme.Name == "Ocean" ? Color.FromArgb("#006064") :
                               Color.FromArgb("#c06c00");

        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                int val = gameLogic.Board.gameBoard[r, c].GetValue();
                var container = (Grid)gr4x4.Children.Cast<View>().First(v => Grid.GetRow(v) == r && Grid.GetColumn(v) == c);

                var pesa = (BoxView)container.Children[0];
                var valgeKast = (BoxView)container.Children[1];
                var pilt = (Image)container.Children[2];
                var tekst = (Label)container.Children[3];

                pesa.BackgroundColor = currentTheme.EmptyTileColor;
                tekst.TextColor = currentTheme.TextColor;

                bool oliPeidus = !valgeKast.IsVisible;

                if (val > 0)
                {
                    valgeKast.BackgroundColor = currentTheme.Name == "Neon" ? Colors.Black : Colors.White;
                    valgeKast.IsVisible = true;
                    pilt.IsVisible = currentTheme.Name == "Kalapulk";
                    tekst.Text = val.ToString();

                    if (oliPeidus)
                    {
                        TeeTekkimisAnimatsioon(container);
                    }
                }
                else
                {
                    valgeKast.IsVisible = false;
                    pilt.IsVisible = false;
                    tekst.Text = "";
                }
            }
        }

        if (this.Resources["ScoreLabel"] is Label scoreLabel)
        {
            int currentScore = gameLogic.Board.GetScore();
            scoreLabel.Text = $"SCORE: {currentScore}";
            scoreLabel.TextColor = currentTheme.TextColor;
            SaveScore(currentScore);
        }
    }

    private void LisaSõrmeLiigutused()
    {
        var directions = new[] { SwipeDirection.Up, SwipeDirection.Down, SwipeDirection.Left, SwipeDirection.Right };
        foreach (var dir in directions)
        {
            var swipe = new SwipeGestureRecognizer { Direction = dir };
            swipe.Swiped += OnSwiped;
            gr4x4.GestureRecognizers.Add(swipe);
        }
    }

    private async void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (gameLogic.ProcessMove(e.Direction.ToString()))
        {
            UuendaVisuaal();
            if (gameLogic.Board.IsGameOver())
            {
                await HandleGameOver();
            }
        }
    }

    private async Task HandleGameOver()
    {
        bool restart = await DisplayAlert("GAME OVER",
            $"No moves left! Your score: {gameLogic.Board.GetScore()}",
            "Restart", "Exit");

        if (restart)
        {
            gameLogic.Board.ResetGame();
            UuendaVisuaal();
        }
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        string category = await DisplayActionSheet("SETTINGS", "Cancel", null, "THEMES", "CUSTOM COLORS");

        if (category == "THEMES")
        {
            string action = await DisplayActionSheet("CHANGE THEME", "Cancel", null, "FISH FINGERS", "DEEP OCEAN", "NEON MATRIX");
            switch (action)
            {
                case "FISH FINGERS": SetTheme("Kalapulk"); break;
                case "DEEP OCEAN": SetTheme("Ocean"); break;
                case "NEON MATRIX": SetTheme("Neon"); break;
            }
        }
        else if (category == "CUSTOM COLORS")
        {
            string color = await DisplayActionSheet("TEXT COLOR", "Cancel", null, "Red", "Green", "Blue", "White", "Black");
            if (color != "Cancel") currentTheme.TextColor = GetColorFromString(color);
        }

        currentTheme.Apply(this);
        UuendaVisuaal();
    }

    private Color GetColorFromString(string colorName)
    {
        return colorName switch
        {
            "Red" => Colors.Red,
            "Green" => Colors.Lime,
            "Blue" => Colors.DeepSkyBlue,
            "White" => Colors.White,
            _ => Colors.Black
        };
    }

    private void SetTheme(string themeName)
    {
        if (themeName == "Kalapulk")
            currentTheme = new Theme { Name = "Kalapulk", BackgroundColor = Color.FromArgb("#fff5e6"), EmptyTileColor = Color.FromArgb("#ed9121"), TextColor = Colors.Red };
        else if (themeName == "Ocean")
            currentTheme = new Theme { Name = "Ocean", BackgroundColor = Color.FromArgb("#e0f7fa"), EmptyTileColor = Color.FromArgb("#00838f"), TextColor = Colors.Black };
        else if (themeName == "Neon")
            currentTheme = new Theme { Name = "Neon", BackgroundColor = Color.FromArgb("#0a0a0a"), EmptyTileColor = Color.FromArgb("#1a1a1a"), TextColor = Color.FromArgb("#00ff00") };
    }

    private async void TeeTekkimisAnimatsioon(View element)
    {
        element.Scale = 0;
        await element.ScaleTo(1, 200, Easing.CubicOut);
    }
}