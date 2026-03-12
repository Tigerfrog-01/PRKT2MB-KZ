namespace Projekt2_TARpe24_Kristopher;

public partial class PickerImagePage : ContentPage
{
    Grid gr4x1, gr3x3;
    Picker picker;
    Image img;
    Switch s_pilt, s_grid;
    Random rnd = new Random();

    public PickerImagePage()
    {
        
        
        gr4x1 = new Grid
        {
            RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
            },
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            },
        };

       
        picker = new Picker
        {
            Title = "Vali pilt",
            ItemsSource = new List<string> { "Pilt 1", "Pilt 2", "Pilt 3" },
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        picker.SelectedIndexChanged += Piltide_valik;

        
        img = new Image
        {
            Source = "dotnet_bot.png",
            HorizontalOptions = LayoutOptions.Center,
        };

 
        s_pilt = new Switch
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsToggled = true
        };
        s_pilt.Toggled += (sender, e) => img.IsVisible = e.Value;

      
        s_grid = new Switch
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsToggled = false
        };
        s_grid.Toggled += (sender, e) =>
        {
            if (e.Value)
            {
                gr3x3 = Täida_gr3x3();
                gr4x1.Add(gr3x3, 0, 2);
                gr4x1.SetColumnSpan(gr3x3, 2);
            }
            else
            {
            
                if (gr4x1.Contains(gr3x3))
                {
                    gr4x1.Remove(gr3x3);
                }
            }
        };

  
        gr4x1.Add(picker, 0, 0);
        gr4x1.SetColumnSpan(picker, 2);

        gr4x1.Add(img, 0, 1);
        gr4x1.SetColumnSpan(img, 2);

        gr4x1.Add(s_pilt, 0, 3);
        gr4x1.Add(s_grid, 1, 3);

      
        Content = gr4x1;

       
        InitializeComponent();
    }

    private void Piltide_valik(object? sender, EventArgs e)
    {
        if (picker.SelectedIndex == -1) return;
        if (picker.SelectedIndex == 0) img.Source = "kalapulk1.png";
        else if (picker.SelectedIndex == 1) img.Source = "thumb.png";
        else if (picker.SelectedIndex == 2) img.Source = "dotnet_bot.png";
    }

    private Grid Täida_gr3x3()
    {
        Grid tempGrid = new Grid();
        for (int i = 0; i < 3; i++)
        {
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                BoxView kast = new BoxView
                {
                    BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                    Margin = 2 
                };

                int rida = r;
                int veerg = c;
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, args) =>
                {
                    kast.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    await DisplayAlertAsync("Koordinaadid", $"Vajutasid lahtrisse:\nRida: {rida}\nVeerg: {veerg}", "Selge");
                };
                kast.GestureRecognizers.Add(tap);
                tempGrid.Add(kast, c, r);
            }
        }
        return tempGrid;
    }
}