using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using static Projekt2_TARpe24_Kristopher.Karusell;

namespace Projekt2_TARpe24_Kristopher;

public partial class CityExplorer : ContentPage
{
    private ObservableCollection<CarouselItem> items;
    private CarouselView carouselView;
    public CityExplorer()
	{



        Title = "Linna avastaja";
        BackgroundColor = Color.FromArgb("#F5F5F5");


        InitializeComponent();

        items = new ObservableCollection<CarouselItem>
            {
                new CarouselItem { Title = "Vanalinn", ImageUrl = "vanalinn.jpg",Description = "12 sajandi vanune vanalinn millel on rikkalik ajalookiht"},
                new CarouselItem { Title = "Kadrioru park", ImageUrl = "park.jpeg", Description = "Elegantne park mis avati 18 sajandil, seal kasvab tammed, kirsipuud, roosid ja seal asub palee"},
                new CarouselItem { Title = "Draakoni Restorant", ImageUrl = "resto.jpg",Description ="Tahad kogeda mida inimeset sõid keskajal? Draakoni restorant on just see koht kus rändad ajas tagasi" },
                new CarouselItem { Title = "Linnaelu muuseum", ImageUrl = "muuseum.jpg",Description="Koge Tallinna linnaelu läbi mitme sajandite ja saa uusi teadmisi" },
                new CarouselItem { Title = "Estonia teater", ImageUrl = "teater.jpg", Description="Eesti ikoonilisem teater, mis tasub külastada, saad näha siin Eesti teatri kunstiteost"}
            };

        carouselView = new CarouselView
        {
            ItemsSource = items,
            HeightRequest = 450,
            PeekAreaInsets = new Thickness(40, 0, 40, 0),
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame { CornerRadius = 20, Padding = 0, Margin = 10, BackgroundColor = Colors.Black, IsClippedToBounds = true };
                var grid = new Grid();
                var image = new Image { Aspect = Aspect.AspectFill };
                image.SetBinding(Image.SourceProperty, "ImageUrl");

                var infoStack = new VerticalStackLayout { VerticalOptions = LayoutOptions.End, Padding = 20 };
                var titleLabel = new Label { TextColor = Colors.White, FontSize = 22, FontAttributes = FontAttributes.Bold };
                titleLabel.SetBinding(Label.TextProperty, "Title");
                var descLabel = new Label { TextColor = Colors.LightGray, FontSize = 14, MaxLines = 2 };
                descLabel.SetBinding(Label.TextProperty, "Description");

                infoStack.Children.Add(titleLabel);
                infoStack.Children.Add(descLabel);
                grid.Children.Add(image);
                grid.Children.Add(infoStack);

                var tap = new TapGestureRecognizer();
                tap.Tapped += async (s, e) => {
                    var tappedItem = (s as Frame).BindingContext as CarouselItem;
                    if (tappedItem != null) await DisplayAlert(tappedItem.Title, tappedItem.Description, "OK");
                };
                frame.GestureRecognizers.Add(tap);
                frame.Content = grid;
                return frame;
            })
        };



        var mainStack = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children = {new BoxView { HeightRequest = 1, Color = Colors.Gray }, carouselView }
        };

        var mainGrid = new Grid();
        mainGrid.Children.Add(new ScrollView { Content = mainStack });

        Content = mainGrid;
    }
}