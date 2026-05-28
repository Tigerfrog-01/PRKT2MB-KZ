using Projekt2_TARpe24_Kristopher.Services;

namespace Projekt2_TARpe24_Kristopher.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    public Command<string> ChangeLanguageCommand { get; }

    public SettingsViewModel(LocalizationService localization) : base(localization)
    {
        ChangeLanguageCommand = new Command<string>(ChangeLanguage);
    }

    public string SettingsTitle => Localization.Get("SettingsTitle");
    public string SettingsIntro => Localization.Get("SettingsIntro");
    public string LanguageTitle => Localization.Get("LanguageTitle");
    public string EstonianLanguage => Localization.Get("EstonianLanguage");
    public string EnglishLanguage => Localization.Get("EnglishLanguage");
    public string RussianLanguage => Localization.Get("RussianLanguage");
    public string CurrentLanguageText => string.Format(Localization.Get("CurrentLanguageFormat"), GetLanguageName());

    protected override void UpdateTexts()
    {
        OnPropertyChanged(nameof(SettingsTitle));
        OnPropertyChanged(nameof(SettingsIntro));
        OnPropertyChanged(nameof(LanguageTitle));
        OnPropertyChanged(nameof(EstonianLanguage));
        OnPropertyChanged(nameof(EnglishLanguage));
        OnPropertyChanged(nameof(RussianLanguage));
        OnPropertyChanged(nameof(CurrentLanguageText));
    }

    private void ChangeLanguage(string language)
    {
        Localization.SetCulture(language);
    }

    private string GetLanguageName()
    {
        return Localization.CurrentCulture.TwoLetterISOLanguageName switch
        {
            "en" => EnglishLanguage,
            "ru" => RussianLanguage,
            _ => EstonianLanguage
        };
    }
}
