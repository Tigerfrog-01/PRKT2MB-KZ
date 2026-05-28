using Projekt2_TARpe24_Kristopher.Models;

namespace Projekt2_TARpe24_Kristopher.Services;

public class PlaceCatalog
{
    private readonly LocalizationService localization;

    public PlaceCatalog(LocalizationService localization)
    {
        this.localization = localization;
    }

    public List<Category> GetCategories()
    {
        return new List<Category>
        {
            new() { Key = "history", Emoji = "🏰", Title = localization.Get("CategoryHistory") },
            new() { Key = "parks", Emoji = "🌳", Title = localization.Get("CategoryParks") },
            new() { Key = "food", Emoji = "🍽️", Title = localization.Get("CategoryFood") },
            new() { Key = "culture", Emoji = "🎭", Title = localization.Get("CategoryCulture") }
        };
    }

    public List<Place> GetPlaces(string categoryKey)
    {
        return GetAllPlaces().Where(place => place.CategoryKey == categoryKey).ToList();
    }

    public List<Place> GetAllPlaces()
    {
        return new List<Place>
        {
            CreatePlace("oldtown", "history", "vanalinn.jpg", "PlaceOldTownName", "PlaceOldTownShort", "PlaceOldTownFull"),
            CreatePlace("museum", "history", "muuseum.jpg", "PlaceMuseumName", "PlaceMuseumShort", "PlaceMuseumFull"),
            CreatePlace("kadriorg", "parks", "park.jpg", "PlaceKadriorgName", "PlaceKadriorgShort", "PlaceKadriorgFull"),
            CreatePlace("dragon", "food", "resto.jpg", "PlaceDragonName", "PlaceDragonShort", "PlaceDragonFull"),
            CreatePlace("theater", "culture", "teater.jpg", "PlaceTheaterName", "PlaceTheaterShort", "PlaceTheaterFull")
        };
    }

    private Place CreatePlace(string key, string categoryKey, string imageName, string nameKey, string shortKey, string fullKey)
    {
        return new Place
        {
            Key = key,
            CategoryKey = categoryKey,
            ImageName = imageName,
            Name = localization.Get(nameKey),
            ShortDescription = localization.Get(shortKey),
            FullDescription = localization.Get(fullKey)
        };
    }
}
