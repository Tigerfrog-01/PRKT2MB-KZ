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
            BackgroundImageSource = null;
            BackgroundColor = Colors.Green;
        }
        if (varv == "Punane" || varv == "punane")
        {
            BackgroundImageSource = null;
            BackgroundColor = Colors.Red;
        }
        if (varv == "Kollane" || varv == "kollane")
        {
            BackgroundImageSource = null;
            BackgroundColor = Colors.Yellow;
        }
        if (varv == "Sinine" || varv == "sinine")
        {
            BackgroundImageSource = null;
            BackgroundColor = Colors.Blue;
        }

    }
    private async void KalapulkButton_Clicked(object sender, EventArgs e)
    {
    
        bool result = await DisplayAlert("Kalapulk on parim?", "Kas kalapulk on parim??", "KALAPULK ON PARIMMM", "Kalapulk ei ole parim :c");
        if (result)
            {
                BackgroundImageSource = "kalapulk1.png";
            }
            else
            {

                BackgroundImageSource = "angryface.png";

            }
     
       
            
      
    }
    private async void AlertListButton_Clicked(object? sender, EventArgs e)
    {
      
        var riddles = new List<Riddle>
    {
        new Riddle(" Seest siiru-viiruline, pealt kullakarvaline", "Sibul", "Päike", "Sibul", "Tiiger"),
        new Riddle(" Neli andjat, neli kandjat, kaks koeratõrjujat, üks parmupiits?", "Lehm", "Lehm", "Siga", "Kits"),
        new Riddle(" Hobu hirnub Hiiumaal, hääl kuulub meie maile?", "Äike", "Tuul", "Äike", "Orkaan"),
        new Riddle( " Punane pullike, jõhvist lõake?", "Jõhvikas", "Jõhvikas", "Peet", "Venelane"),
        new Riddle(" Mustem kui süsi, valgem kui lumi, kõrgem kui kirik, madalam kui regi?", "Harakas", "Vares", "Harakas", "Ronk")
    };

        var random = new Random();
        var selectedRiddle = riddles[random.Next(riddles.Count)];

      
        string action = await DisplayActionSheetAsync(
            selectedRiddle.Question,
            "Loobu",
            "Kustutada",
            selectedRiddle.Options
        );

        
        if (action == "Loobu" || action == "Kustutada" || action == null) return;

        if (action == selectedRiddle.CorrectAnswer)
        {
            await DisplayAlertAsync($"Õige, palju õnne {name}!", $"Sa valisid: {action}", "Ok");
        }
        else
        {
            await DisplayAlertAsync($"Vale vastus {name}!", $"Sa valisid: {action}. Õige oli: {selectedRiddle.CorrectAnswer}", "Ok");
        }
    }
    private async void AlertQuestButton_Clicked(object sender, EventArgs e)
    {
   
        var rebusList = new List<Rebus>
    {
        new Rebus("❄️ + 👨", "Talv", "Snowman", "lumemees"),
        new Rebus("Mon + 🔑", "Džungel", "Monkey"),
        new Rebus("Ma + ✅", "Kodu", "Maja"),
        new Rebus("Öö + 🦅", "Tark loom", "Öökull"),
        new Rebus("🐟 + 🪄", "KÕIGE PARIM ASI MAAILMAS", "Kalapulk")
    };

    
        var random = new Random();
        var selected = rebusList[random.Next(rebusList.Count)];

     
        string result = await DisplayPromptAsync("Vasta vastus selle rebusele " + name, selected.EmojiPrompt, placeholder: selected.Placeholder);

       
        if (string.IsNullOrWhiteSpace(result)) return;

      
        if (selected.ValidAnswers.Contains(result.Trim().ToLower()))
        {
            await DisplayAlertAsync($"Sinu valik oli Õige {name}", $"Sa valisid vastuse: {result}", "Ok");
        }
        else
        {
            await DisplayAlertAsync($"Sinu valik oli VALE {name}", $"Sa valisid vastuse: {result}", "Ok");
        }
    }
    public class Riddle
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] Options { get; set; }

        public Riddle(string q, string correct, params string[] options)
        {
            Question = q;
            CorrectAnswer = correct;
            Options = options;
        }
    }

    public class Rebus
    {
        public string EmojiPrompt { get; set; }
        public string Placeholder { get; set; }
        public string[] ValidAnswers { get; set; }

        public Rebus(string prompt, string placeholder, params string[] answers)
        {
            EmojiPrompt = prompt;
            Placeholder = placeholder;
       
            ValidAnswers = answers.Select(a => a.ToLower()).ToArray();
        }
    }
}