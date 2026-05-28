using Projekt2_TARpe24_Kristopher.ViewModels;

namespace Projekt2_TARpe24_Kristopher.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadFavoritesAsync();
    }
}
