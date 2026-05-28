using System.Collections.ObjectModel;
using Projekt2_TARpe24_Kristopher.Models;
using Projekt2_TARpe24_Kristopher.Services;

namespace Projekt2_TARpe24_Kristopher.ViewModels;

public class ExploreViewModel : BaseViewModel
{
    private readonly DatabaseService database;
    private readonly PlaceCatalog catalog;
    private Category? selectedCategory;

    public ObservableCollection<Place> Places { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();
    public Command<Category> SelectCategoryCommand { get; }

    public ExploreViewModel(DatabaseService database, PlaceCatalog catalog, LocalizationService localization) : base(localization)
    {
        this.database = database;
        this.catalog = catalog;
        SelectCategoryCommand = new Command<Category>(SelectCategory);
        LoadCategories("history");
    }

    public Category? SelectedCategory
    {
        get => selectedCategory;
        set
        {
            if (SetProperty(ref selectedCategory, value))
            {
                OnPropertyChanged(nameof(SelectedCategoryTitle));
            }
        }
    }

    public string AppTitle => Localization.Get("AppTitle");
    public string ExploreTitle => Localization.Get("ExploreTitle");
    public string ExploreIntro => Localization.Get("ExploreIntro");
    public string SelectedCategoryTitle => SelectedCategory?.Title ?? string.Empty;
    public string AddFavoriteButtonText => Localization.Get("AddFavoriteButton");
    public string CloseButtonText => Localization.Get("CloseButton");
    public string FavoriteSavedTitle => Localization.Get("FavoriteSavedTitle");
    public string FavoriteSavedMessage => Localization.Get("FavoriteSavedMessage");

    public async Task AddFavoriteAsync(Place place)
    {
        await database.SaveFavoriteAsync(place);
    }

    protected override void UpdateTexts()
    {
        LoadCategories(SelectedCategory?.Key ?? "history");
    }

    private void SelectCategory(Category category)
    {
        SelectedCategory = category;
        LoadPlaces(category.Key);
    }

    private void LoadCategories(string selectedKey)
    {
        Categories.Clear();

        foreach (var category in catalog.GetCategories())
        {
            Categories.Add(category);
        }

        SelectedCategory = Categories.FirstOrDefault(category => category.Key == selectedKey) ?? Categories.FirstOrDefault();

        if (SelectedCategory is not null)
        {
            LoadPlaces(SelectedCategory.Key);
        }
    }

    private void LoadPlaces(string categoryKey)
    {
        Places.Clear();

        foreach (var place in catalog.GetPlaces(categoryKey))
        {
            Places.Add(place);
        }
    }
}
