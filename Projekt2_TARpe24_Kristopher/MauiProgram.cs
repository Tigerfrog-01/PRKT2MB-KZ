using Microsoft.Extensions.Logging;
using Projekt2_TARpe24_Kristopher.Services;
using Projekt2_TARpe24_Kristopher.ViewModels;
using Projekt2_TARpe24_Kristopher.Views;

namespace Projekt2_TARpe24_Kristopher
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<LocalizationService>();
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<PlaceCatalog>();

            builder.Services.AddSingleton<MainTabbedPage>();
            builder.Services.AddSingleton<ExplorePage>();
            builder.Services.AddSingleton<FavoritesPage>();
            builder.Services.AddSingleton<SettingsPage>();

            builder.Services.AddSingleton<ExploreViewModel>();
            builder.Services.AddSingleton<FavoritesViewModel>();
            builder.Services.AddSingleton<SettingsViewModel>();

            return builder.Build();
        }
    }
}
