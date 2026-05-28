using System.ComponentModel;
using System.Runtime.CompilerServices;
using Projekt2_TARpe24_Kristopher.Services;

namespace Projekt2_TARpe24_Kristopher.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
    protected readonly LocalizationService Localization;

    public event PropertyChangedEventHandler? PropertyChanged;

    public BaseViewModel(LocalizationService localization)
    {
        Localization = localization;
        Localization.LanguageChanged += OnLanguageChanged;
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(name);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string name = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    protected virtual void UpdateTexts()
    {
    }

    private void OnLanguageChanged()
    {
        UpdateTexts();
        OnPropertyChanged(string.Empty);
    }
}
