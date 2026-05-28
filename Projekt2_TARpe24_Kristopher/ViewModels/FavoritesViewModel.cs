using System.Collections.ObjectModel;
using Projekt2_TARpe24_Kristopher.Models;
using Projekt2_TARpe24_Kristopher.Services;

namespace Projekt2_TARpe24_Kristopher.ViewModels;

public class FavoritesViewModel : BaseViewModel
{
    private readonly DatabaseService database;
    private readonly PlaceCatalog catalog;

    public ObservableCollection<Place> Favorites { get; } = new();
    public Command<Place> RemoveFavoriteCommand { get; }

    public FavoritesViewModel(DatabaseService database, PlaceCatalog catalog, LocalizationService localization) : base(localization)
    {
        this.database = database;
        this.catalog = catalog;
        RemoveFavoriteCommand = new Command<Place>(async place => await RemoveFavoriteAsync(place));
    }

    public string FavoritesTitle => Localization.Get("FavoritesTitle");
    public string FavoritesIntro => Localization.Get("FavoritesIntro");
    public string EmptyFavoritesText => Localization.Get("EmptyFavoritesText");
    public string RemoveButtonText => Localization.Get("RemoveButton");

    public async Task LoadFavoritesAsync()
    {
        var savedPlaces = await database.GetFavoritesAsync();
        var translatedPlaces = catalog.GetAllPlaces().ToDictionary(place => place.Key);
        Favorites.Clear();

        foreach (var savedPlace in savedPlaces)
        {
            if (translatedPlaces.TryGetValue(savedPlace.Key, out var place))
            {
                place.IsFavorite = true;
                Favorites.Add(place);
            }
            else
            {
                Favorites.Add(savedPlace);
            }
        }
    }

    protected override void UpdateTexts()
    {
        _ = LoadFavoritesAsync();
    }

    private async Task RemoveFavoriteAsync(Place place)
    {
        await database.DeleteFavoriteAsync(place);
        Favorites.Remove(place);
    }
}
