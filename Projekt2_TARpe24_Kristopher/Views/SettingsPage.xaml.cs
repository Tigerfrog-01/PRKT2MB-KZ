using Projekt2_TARpe24_Kristopher.ViewModels;

namespace Projekt2_TARpe24_Kristopher.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
