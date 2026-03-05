namespace Projekt2_TARpe24_Kristopher;

public partial class Popup : ContentPage
{
	public Popup()
	{
		InitializeComponent();

       
        
    }


    private async void AlertButton_Clicked(object? sender, EventArgs e)
    {
        
        await DisplayAlertAsync("Teade", "Teil on uus teade", "OK");
    }

 
    private async void AlertYesNoButton_Clicked(object? sender, EventArgs e)
    {
     
        bool result = await DisplayAlertAsync("Kinnitus", "Kas oled kindel?", "Olen kindel", "Ei ole kindel");

     
        await DisplayAlertAsync("Teade", "Teie valik on: " + (result ? "Jah" : "Ei"), "OK");
    }

   
    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {
       
        string action = await DisplayActionSheetAsync("Mida teha?", "Loobu", "Kustutada", "Tantsida", "Laulda", "Joonestada");

        if (action != null && action != "Loobu")
        {
            await DisplayAlertAsync("Valik", "Sa valisid tegevuse: " + action, "OK");
        }
    }

    private async void AlertQuestButton_Clicked(object sender, EventArgs e)
    {
        string result1 = await DisplayPromptAsync("Küsimus", "Kuidas läheb?", placeholder: "Tore!");
        string result2 = await DisplayPromptAsync("Vasta", "Millega võrdub 5 + 5?", initialValue: "10", maxLength: 2, keyboard: Keyboard.Numeric);
    }





}