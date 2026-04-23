using Microsoft.Maui.Controls;
using Projekt2_TARpe24_Kristopher; 
using System.Collections.ObjectModel;
using System.Globalization;

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

        private CarouselView carouselView;
        private ObservableCollection<CarouselItem> items;
        private int position = 0;

        public Karusell()
        {
         
            Title = AppResources.PageTitle;

            items = new ObservableCollection<CarouselItem>
            {
                new CarouselItem { Title = "Pizza", ImageUrl = "pizza.jpg", Description = "Napoli stiilis itaalia pärane mahlane maitsev pitsa" },
                new CarouselItem { Title = "Bastoncini di Pesce", ImageUrl = "fishfingers.jpg", Description = "Vastupanduvad Bastocini, mis on kohaliku itaalia varjatud pärl" },
                new CarouselItem { Title = "Lasanje", ImageUrl = "lasagna.jpg", Description = "Nii kihiline, sellist maitselist elamust sa oma elus kunagi ei saa" },
                new CarouselItem { Title = "Tiramisu", ImageUrl = "tiramisu.jpg", Description = "Nagu Eesti lemmik küpsise kook aga mõrudam ja depresiivsem" },
                new CarouselItem { Title = "Carbonara", ImageUrl = "carbonara.jpg", Description = "Kooreline pasta koos krõbeda peekoniga, nii krõbe.." }
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
            btnEn.Clicked += (s, e) => ChangeLanguage("en-US");
            var btnEt = new Button { Text = "ET", WidthRequest = 60 };
            btnEt.Clicked += (s, e) => ChangeLanguage("et-EE");
            var langStack = new HorizontalStackLayout { Spacing = 10, HorizontalOptions = LayoutOptions.Center, Children = { btnEt, btnEn } };

            var pealkiriEntry = new Entry { Placeholder = AppResources.NamePlaceHolder };
            var kirjeldusEntry = new Entry { Placeholder = AppResources.DescPlaceHolder };
            var imageUrlEntry = new Entry { Placeholder = AppResources.UrlPlaceHolder };

            var lisaNupp = new Button
            {
                Text = AppResources.AddBtn,
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
                    ImageUrl = string.IsNullOrWhiteSpace(imageUrlEntry.Text) ? "https://loremflickr.com/600/400/food" : imageUrlEntry.Text
                });

                pealkiriEntry.Text = string.Empty;
                kirjeldusEntry.Text = string.Empty;
                imageUrlEntry.Text = string.Empty;
                carouselView.Position = items.Count - 1;
            };

            Device.StartTimer(TimeSpan.FromSeconds(4), () =>
            {
                if (items.Count == 0) return false;
                position = (carouselView.Position + 1) % items.Count;
                carouselView.Position = position;
                return true;
            });

            Content = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Padding = 20,
                    Spacing = 10,
                    Children = {
                        langStack,
                        carouselView,
                        indicatorView,
                        new BoxView { HeightRequest = 1, Color = Colors.Gray },
                        pealkiriEntry,
                        kirjeldusEntry,
                        imageUrlEntry,
                        lisaNupp
                    }
                }
            };
        }

        private void ChangeLanguage(string langCode)
        {
            var culture = new CultureInfo(langCode);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            if (langCode == "en-US")
            {
            
                Title = AppResourcesEN.PageTitle;
            }
            else
            {
                Title = AppResources.PageTitle;
            }

            Application.Current.MainPage = new NavigationPage(new Karusell());
        }
    }
}