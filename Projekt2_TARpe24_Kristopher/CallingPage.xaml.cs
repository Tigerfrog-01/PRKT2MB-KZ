namespace Projekt2_TARpe24_Kristopher;

public partial class CallingPage : ContentPage
{
	public CallingPage(string tel, string imagePath)
	{
		InitializeComponent();

        BackgroundColor = Colors.Black;
        NavigationPage.SetHasNavigationBar(this, false); 

        var profileImage = new Image
        {
            Source = imagePath,
            HeightRequest = 250,
            WidthRequest = 250,
            Aspect = Aspect.AspectFill,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 100, 0, 20)
        };

        var nameLabel = new Label
        {
            Text = tel,
            TextColor = Colors.White,
            FontSize = 30,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, 100)
        };

        var declineButton = new Button
        {
            Text = "X", 
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            FontSize = 24,
            HeightRequest = 80,
            WidthRequest = 80,
            CornerRadius = 40, 
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, 0, 50)
        };

        declineButton.Clicked += async (s, e) =>
        {
            await Navigation.PushAsync(new Phone());
        };

        Content = new VerticalStackLayout
        {
            Children = { profileImage, nameLabel, declineButton }
        };
    }
}