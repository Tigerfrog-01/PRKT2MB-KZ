namespace Projekt2_TARpe24_Kristopher;

public partial class PopUpProject : ContentPage
{
	public PopUpProject()
	{
		InitializeComponent();
	}
    string name;
    

    private async void NimiButton_Clicked(object sender, EventArgs e)
    {
         name = await DisplayPromptAsync("Tere!!!", "Mis su nimi on?", placeholder: "TEREEE!");
    
    }

    private async void VarvButton_Clicked(object sender, EventArgs e)
    {
        string varv = await DisplayPromptAsync("Tere!!!" + name, "Mis su lemmik värv on?", placeholder: "Vali värv!❤️💙💚💛");
        if (varv == "Roheline" || varv == "roheline")
        {
            BackgroundColor = Colors.Green;
        }
        if (varv == "Punane" || varv == "punane")
        {
            BackgroundColor = Colors.Red;
        }
        if (varv == "Kollane" || varv == "kollane")
        {
            BackgroundColor = Colors.Yellow;
        }
        if (varv == "Sinine" || varv == "sinine")
        {
            BackgroundColor = Colors.Blue;
        }

    }
    private async void KalapulkButton_Clicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlertAsync("Kalapulk on parim?", "Kas kalapulk on parim??", "KALAPULK ON PARIMMM", "Kalapulk ei ole parim :c");
        if (result == true)
        {
            BackgroundImageSource = "kalapulk.png";
            BackgroundColor = Colors.Yellow;
        }
        else 
        {
            BackgroundColor = Colors.Red;
        }

    }
    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {

        string action = await DisplayActionSheetAsync("Seest siiru-viiruline, pealt kullakarvaline", "Loobu", "Kustutada", "Päike", "Sibul", "Tiiger");

        if (action == "Päike" )
        {
            await DisplayAlertAsync("Sinu valik oli vale " + name, "Sa valisid vastuse: " + action, "Ok");
        }
        if (action == "Sibul")
        {
            await DisplayAlertAsync("Sinu valik oli õige, palju õnne! " + name, "Sa valisid vastuse: " + action, "Ok");
        }
        if (action == "Tiiger")
        {
            await DisplayAlertAsync("Sinu valik oli vale " + name , "Sa valisid vastuse: " + action, "Ok");
        }
    }
    private async void AlertQuestButton_Clicked(object sender, EventArgs e)
    {
        string result1 = await DisplayPromptAsync("Vasta vastus selle rebusele " + name, "❄️ + 👨", placeholder: "Talv");

        if (result1 == "Snowman" || result1 == "snowman" || result1 == "lumemees" || result1 == "Lumemees")
        {
            await DisplayAlertAsync("Sinu valik oli Õige " + name, "Sa valisid vastuse: " + result1, "Ok");
        }
        else
        {
            await DisplayAlertAsync("Sinu valik oli VALE " + name, "Sa valisid vastuse: " + result1, "Ok");
        }
        string result2 = await DisplayPromptAsync("Vasta vastus selle rebusele", "Mon + 🔑", placeholder: "Džungel");

        if (result2 == "Monkey" || result2 == "monkey")
        {
            await DisplayAlertAsync("Sinu valik oli Õige " + name, "Sa valisid vastuse: " + result2, "Ok");
        }
        else
        {
            await DisplayAlertAsync("Sinu valik oli VALE " + name, "Sa valisid vastuse: " + result2, "Ok");
        }
    }
}