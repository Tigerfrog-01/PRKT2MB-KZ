using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Projekt2_TARpe24_Kristopher;

public partial class DisasterRecipe : ContentPage
{
    Entry OmadusEntry;
    Entry KategooriEntry;
    Entry LinkEntry;
    Image RetseptiPilt;
    string valitudPildiRada = "";

    public DisasterRecipe()
    {
        InitializeComponent();

        BackgroundColor = Color.FromArgb("#F5F5F5");
        Title = "Uus retsept";

        var header = new Label
        {
            Text = "Retsepti Lisamine",
            FontSize = 28,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.Black
        };

        OmadusEntry = new Entry { Placeholder = "Toidu nimi", TextColor = Colors.Black, PlaceholderColor = Colors.Gray };
        KategooriEntry = new Entry { Placeholder = "Kategooria (nt. Magustoit)", TextColor = Colors.Black, PlaceholderColor = Colors.Gray };
        LinkEntry = new Entry { Placeholder = "Pildi veebiaadress (URL)", TextColor = Colors.Black, PlaceholderColor = Colors.Gray };

        var pildiNupp = new Button
        {
            Text = "VALI PILT GALERIIST",
            BackgroundColor = Colors.SlateGray,
            TextColor = Colors.White,
            CornerRadius = 10
        };

        pildiNupp.Clicked += async (s, e) =>
        {
            var pilt = await MediaPicker.Default.PickPhotoAsync();
            if (pilt != null)
            {
                valitudPildiRada = pilt.FullPath;
                LinkEntry.Text = valitudPildiRada;
                RetseptiPilt.Source = ImageSource.FromFile(valitudPildiRada);
                RetseptiPilt.IsVisible = true;
            }
        };

        RetseptiPilt = new Image { HeightRequest = 150, Aspect = Aspect.AspectFill, IsVisible = false };

        var lisaNupp = new Button
        {
            Text = "SALVESTA RETSEPT",
            BackgroundColor = Colors.ForestGreen,
            TextColor = Colors.White,
            CornerRadius = 25,
            HeightRequest = 50,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(0, 15)
        };

        lisaNupp.Clicked += async (s, e) =>
        {
            if (string.IsNullOrWhiteSpace(OmadusEntry.Text) ||
                string.IsNullOrWhiteSpace(KategooriEntry.Text) ||
                string.IsNullOrWhiteSpace(LinkEntry.Text))
            {
                await DisplayAlert("Hoiatus", "Kõik väljad peavad olema täidetud!", "OK");
                return;
            }

            var uus = new Retsept { Nimi = OmadusEntry.Text, Kategooria = KategooriEntry.Text, PildiLink = LinkEntry.Text };
            FailiHaldur.SalvestaRetsept(uus);

            OmadusEntry.Text = "";
            KategooriEntry.Text = "";
            LinkEntry.Text = "";
            RetseptiPilt.IsVisible = false;

            await DisplayAlert("Edukas", "Retsept on salvestatud!", "OK");
        };

        Button recipeBtn = new Button
        {
            Text = "Ava retsepti raamat",
            BackgroundColor = Colors.DarkSlateBlue,
            TextColor = Colors.White,
            FontSize = 24,
            HeightRequest = 60,
            Margin = new Thickness(0, 20, 0, 0)
        };

        recipeBtn.Clicked += async (s, e) => await Navigation.PushAsync(new RecipeListPage());

  
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 12,
                Children =
                {
                    header,
                    OmadusEntry,
                    KategooriEntry,
                    LinkEntry,
                    pildiNupp,
                    RetseptiPilt,
                    lisaNupp,
                    recipeBtn
                }
            }
        };
    }
}