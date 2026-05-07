using Microsoft.Maui.Controls;
using System.Linq;

namespace Projekt2_TARpe24_Kristopher;

public partial class RecipeListPage : ContentPage
{
    ListView RetseptiLoend;

    public RecipeListPage()
    {
        Title = "Minu retseptid";
        BackgroundColor = Color.FromArgb("#F5F5F5");

        RetseptiLoend = new ListView
        {
            IsGroupingEnabled = true,
            HasUnevenRows = true,
            GroupHeaderTemplate = new DataTemplate(() =>
            {
                var label = new Label { TextColor = Colors.White, FontAttributes = FontAttributes.Bold, FontSize = 18, Padding = 10 };
                label.SetBinding(Label.TextProperty, "Nimetus");
                return new ViewCell { View = new Frame { BackgroundColor = Colors.ForestGreen, Padding = 0, CornerRadius = 0, Content = label } };
            }),
            ItemTemplate = new DataTemplate(() =>
            {
                var grid = new Grid { ColumnDefinitions = { new ColumnDefinition(80), new ColumnDefinition(GridLength.Star) }, Padding = 10 };

                var pilt = new Image { WidthRequest = 70, HeightRequest = 70, Aspect = Aspect.AspectFill };
                pilt.SetBinding(Image.SourceProperty, "PildiLink");

                var nimi = new Label { FontAttributes = FontAttributes.Bold, FontSize = 16, VerticalOptions = LayoutOptions.Center, TextColor = Colors.Black };
                nimi.SetBinding(Label.TextProperty, "Nimi");

                grid.Add(pilt, 0, 0);
                grid.Add(nimi, 1, 0);

                var deleteItem = new SwipeItem
                {
                    Text = "Kustuta",
                    BackgroundColor = Colors.Red,
                    Command = new Command((item) =>
                    {
                        var r = item as Retsept;
                        KustutaRetsept(r);
                    })
                };
                deleteItem.SetBinding(SwipeItem.CommandParameterProperty, ".");

                var swipeView = new SwipeView
                {
                    RightItems = new SwipeItems { deleteItem },
                    Content = new Frame
                    {
                        BorderColor = Colors.LightGray,
                        CornerRadius = 10,
                        Padding = 0,
                        Margin = new Thickness(5, 2),
                        BackgroundColor = Color.FromArgb("#FAFAFA"),
                        Content = grid
                    }
                };

                return new ViewCell { View = swipeView };
            })
        };

        Content = RetseptiLoend;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LaadiAndmed();
    }

    private void LaadiAndmed()
    {
        try
        {
            var retseptid = FailiHaldur.LoeRetseptid();

            if (retseptid == null) return;

            var grupeeritud = retseptid
                .Where(r => !string.IsNullOrEmpty(r.Kategooria))
                .GroupBy(r => r.Kategooria)
                .Select(g => new RetseptiKategooria(g.Key, g.ToList()))
                .OrderBy(g => g.Nimetus)
                .ToList();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                RetseptiLoend.ItemsSource = null;
                RetseptiLoend.ItemsSource = grupeeritud;
            });
        }
        catch
        {
        }
    }

    private void KustutaRetsept(Retsept r)
    {
        if (r == null) return;
        var nimekiri = FailiHaldur.LoeRetseptid();
        var eemaldatav = nimekiri.FirstOrDefault(x => x.Nimi == r.Nimi && x.Kategooria == r.Kategooria);
        if (eemaldatav != null)
        {
            nimekiri.Remove(eemaldatav);
            FailiHaldur.SalvestaKõik(nimekiri);
            LaadiAndmed();
        }
    }
}