using Projekt2_TARpe24_Kristopher.Models;
using Projekt2_TARpe24_Kristopher.ViewModels;

namespace Projekt2_TARpe24_Kristopher.Views;

public partial class ExplorePage : ContentPage
{
    private readonly ExploreViewModel viewModel;
    private IDispatcherTimer? timer;

    public ExplorePage(ExploreViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        timer ??= Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(4);
        timer.Tick -= MoveCarousel;
        timer.Tick += MoveCarousel;
        timer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer?.Stop();
    }

    private async void PlaceTapped(object sender, TappedEventArgs e)
    {
        if ((sender as BindableObject)?.BindingContext is not Place place)
        {
            return;
        }

        var add = await DisplayAlert(place.Name, place.FullDescription, viewModel.AddFavoriteButtonText, viewModel.CloseButtonText);

        if (add)
        {
            await viewModel.AddFavoriteAsync(place);
            await DisplayAlert(viewModel.FavoriteSavedTitle, viewModel.FavoriteSavedMessage, viewModel.CloseButtonText);
        }
    }

    private void MoveCarousel(object? sender, EventArgs e)
    {
        if (viewModel.Places.Count < 2)
        {
            return;
        }

        PlacesCarousel.Position = (PlacesCarousel.Position + 1) % viewModel.Places.Count;
    }
}
