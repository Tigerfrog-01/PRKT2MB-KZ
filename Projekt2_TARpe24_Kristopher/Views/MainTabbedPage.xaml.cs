namespace Projekt2_TARpe24_Kristopher.Views;

public partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage(ExplorePage explorePage, FavoritesPage favoritesPage, SettingsPage settingsPage)
    {
        InitializeComponent();
        Children.Add(explorePage);
        Children.Add(favoritesPage);
        Children.Add(settingsPage);
    }
}
