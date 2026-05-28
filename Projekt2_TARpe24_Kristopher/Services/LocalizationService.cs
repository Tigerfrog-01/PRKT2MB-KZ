using System.Globalization;
using System.Resources;

namespace Projekt2_TARpe24_Kristopher.Services;

public class LocalizationService
{
    private readonly ResourceManager resourceManager = new("Projekt2_TARpe24_Kristopher.Resources.Strings.AppResources", typeof(App).Assembly);

    public event Action? LanguageChanged;

    public CultureInfo CurrentCulture { get; private set; }

    public LocalizationService()
    {
        var language = Preferences.Default.Get("AppLanguage", "et");
        CurrentCulture = new CultureInfo(language);
        ApplyCulture(CurrentCulture);
    }

    public string Get(string key)
    {
        return resourceManager.GetString(key, CurrentCulture) ?? key;
    }

    public void SetCulture(string language)
    {
        CurrentCulture = new CultureInfo(language);
        Preferences.Default.Set("AppLanguage", language);
        ApplyCulture(CurrentCulture);
        LanguageChanged?.Invoke();
    }

    private static void ApplyCulture(CultureInfo culture)
    {
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;
    }
}
