namespace Projekt2_TARpe24_Kristopher;

public partial class PopUpProject : ContentPage
{
	public PopUpProject()
	{
		InitializeComponent();
	}
    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {

        string action = await DisplayActionSheetAsync("Seest siiru-viiruline, pealt kullakarvaline", "Loobu", "Kustutada", "Päike", "Sibul", "Tiiger");

        if (action == "Päike" )
        {
            await DisplayAlertAsync("Sinu valik oli vale", "Sa valisid vastuse: " + action, "Ok");
        }
        if (action == "Sibul")
        {
            await DisplayAlertAsync("Sinu valik oli õige, palju õnne!", "Sa valisid vastuse: " + action, "Ok");
        }
        if (action == "Tiiger")
        {
            await DisplayAlertAsync("Sinu valik oli vale", "Sa valisid vastuse: " + action, "Ok");
        }
    }
    private async void AlertQuestButton_Clicked(object sender, EventArgs e)
    {
        string result1 = await DisplayPromptAsync("Vasta vastus selle rebusele", "❄️ + 👨", placeholder: "Talv");

        if (result1 == "Snowman" || result1 == "snowman" || result1 == "lumemees" || result1 == "Lumemees")
        {
            await DisplayAlertAsync("Sinu valik oli Õige", "Sa valisid vastuse: " + result1, "Ok");
        }
        else
        {
            await DisplayAlertAsync("Sinu valik oli VALE", "Sa valisid vastuse: " + result1, "Ok");
        }
        string result2 = await DisplayPromptAsync("Vasta vastus selle rebusele", "Mon + 🔑", placeholder: "Džungel");

        if (result2 == "Monkey" || result2 == "monkey")
        {
            await DisplayAlertAsync("Sinu valik oli Õige", "Sa valisid vastuse: " + result2, "Ok");
        }
        else
        {
            await DisplayAlertAsync("Sinu valik oli VALE", "Sa valisid vastuse: " + result2, "Ok");
        }
    }
}