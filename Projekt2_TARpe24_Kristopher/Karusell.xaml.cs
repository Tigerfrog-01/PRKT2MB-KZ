using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Projekt2_TARpe24_Kristopher
{
    public partial class Karusell : ContentPage
    {
        public class CarouselItem
        {
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public string Description { get; set; }
        }

        private static string currentLanguage = "et";
        private CarouselView carouselView;
        private ObservableCollection<CarouselItem> items;
        private int position = 0;

        private Entry pealkiriEntry;
        private Entry kirjeldusEntry;
        private Entry imageUrlEntry;
        private Button lisaNupp;
        private Label lisaSilt;
        private Image backgroundImage;

        private readonly Dictionary<string, Dictionary<string, string>> translations = new()
        {
            ["et"] = new()
            {
                ["PageTitle"] = "Toidumenüü Karussell",
                ["NamePlace"] = "Toidu nimi...",
                ["DescPlace"] = "Kirjeldus...",
                ["UrlPlace"] = "Pildi link...",
                ["AddBtn"] = "Lisa uus kaart",
                ["AddLabel"] = "Lisa oma toit:",
                ["PizzaDesc"] = "Napoli stiilis itaalia pärane mahlane maitsev pitsa",
                ["FishDesc"] = "Vastupanduvad Bastocini, mis on kohaliku itaalia varjatud pärl",
                ["LasagnaDesc"] = "Nii kihiline, sellist maitselist elamust sa oma elus kunagi ei saa",
                ["TiramisuDesc"] = "Nagu Eesti lemmik küpsise kook aga mõrudam ja depresiivsem",
                ["PastaDesc"] = "Kooreline pasta koos krõbeda peekoniga, nii krõbe.."
            },
            ["en"] = new()
            {
                ["PageTitle"] = "Food Menu Carousel",
                ["NamePlace"] = "Food name...",
                ["DescPlace"] = "Description...",
                ["UrlPlace"] = "Image URL...",
                ["AddBtn"] = "Add new card",
                ["AddLabel"] = "Add your food:",
                ["PizzaDesc"] = "Authentic Neapolitan style juicy and delicious pizza",
                ["FishDesc"] = "Irresistible Bastoncini, a hidden gem of local Italian cuisine",
                ["LasagnaDesc"] = "So many layers, you will never have a taste experience like this again",
                ["TiramisuDesc"] = "Like a favorite cookie cake, but more bitter and dramatic",
                ["PastaDesc"] = "Creamy pasta with crispy bacon, oh so crispy.."
            }
        };

        public Karusell()
        {
            Title = translations[currentLanguage]["PageTitle"];

            backgroundImage = new Image
            {
                Source = "background.jpg",
                Aspect = Aspect.AspectFill,
                Opacity = 0.5
            };

            items = new ObservableCollection<CarouselItem>
            {
                new CarouselItem { Title = "Pizza", ImageUrl = "pizza.jpg", Description = translations[currentLanguage]["PizzaDesc"] },
                new CarouselItem { Title = "Bastoncini di Pesce", ImageUrl = "fishfingers.jpg", Description = translations[currentLanguage]["FishDesc"] },
                new CarouselItem { Title = "Lasanje", ImageUrl = "lasagna.jpg", Description = translations[currentLanguage]["LasagnaDesc"] },
                new CarouselItem { Title = "Tiramisu", ImageUrl = "tiramisu.jpg", Description = translations[currentLanguage]["TiramisuDesc"] },
                new CarouselItem { Title = "Carbonara", ImageUrl = "carbonara.jpg", Description = translations[currentLanguage]["PastaDesc"] }
            };

            var indicatorView = new IndicatorView
            {
                IndicatorColor = Colors.LightGray,
                SelectedIndicatorColor = Colors.DarkSlateBlue,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 10)
            };

            carouselView = new CarouselView
            {
                ItemsSource = items,
                HeightRequest = 450,
                PeekAreaInsets = new Thickness(40, 0, 40, 0),
                IndicatorView = indicatorView,
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

            var btnEn = new Button { Text = "EN", WidthRequest = 60 };
            btnEn.Clicked += (s, e) => ChangeLanguage("en");
            var btnEt = new Button { Text = "ET", WidthRequest = 60 };
            btnEt.Clicked += (s, e) => ChangeLanguage("et");
            var langStack = new HorizontalStackLayout { Spacing = 10, HorizontalOptions = LayoutOptions.Center, Children = { btnEt, btnEn } };

            lisaSilt = new Label { Text = translations[currentLanguage]["AddLabel"], FontAttributes = FontAttributes.Bold, Margin = new Thickness(0, 10, 0, 0) };
            pealkiriEntry = new Entry { Placeholder = translations[currentLanguage]["NamePlace"] };
            kirjeldusEntry = new Entry { Placeholder = translations[currentLanguage]["DescPlace"] };
            imageUrlEntry = new Entry { Placeholder = translations[currentLanguage]["UrlPlace"] };

            lisaNupp = new Button
            {
                Text = translations[currentLanguage]["AddBtn"],
                BackgroundColor = Colors.ForestGreen,
                TextColor = Colors.White,
                CornerRadius = 10
            };

            lisaNupp.Clicked += async (s, e) => {
                if (string.IsNullOrWhiteSpace(pealkiriEntry.Text)) return;
                items.Add(new CarouselItem
                {
                    Title = pealkiriEntry.Text,
                    Description = kirjeldusEntry.Text,
                    ImageUrl = string.IsNullOrWhiteSpace(imageUrlEntry.Text) ? "https://loremflickr.com/600/400/italy" : imageUrlEntry.Text
                });
                pealkiriEntry.Text = kirjeldusEntry.Text = imageUrlEntry.Text = string.Empty;
                carouselView.Position = items.Count - 1;
            };

            Device.StartTimer(TimeSpan.FromSeconds(4), () => {
                if (items.Count == 0) return false;
                position = (carouselView.Position + 1) % items.Count;
                carouselView.Position = position;
                return true;
            });

            var mainStack = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 10,
                Children = { langStack, carouselView, indicatorView, new BoxView { HeightRequest = 1, Color = Colors.Gray }, lisaSilt, pealkiriEntry, kirjeldusEntry, imageUrlEntry, lisaNupp }
            };

            var mainGrid = new Grid();
            mainGrid.Children.Add(backgroundImage);
            mainGrid.Children.Add(new ScrollView { Content = mainStack });

            Content = mainGrid;

            StartBackgroundAnimation();
        }

        private async void StartBackgroundAnimation()
        {
            while (true)
            {
                await backgroundImage.ScaleTo(1.2, 10000);
                await backgroundImage.ScaleTo(1.0, 10000);
            }
        }

        private void ChangeLanguage(string langCode)
        {
            currentLanguage = langCode;
            Application.Current.MainPage = new NavigationPage(new Karusell());
        }
    }
}