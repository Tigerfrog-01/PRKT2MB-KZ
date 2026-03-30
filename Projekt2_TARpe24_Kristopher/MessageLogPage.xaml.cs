namespace Projekt2_TARpe24_Kristopher;

public partial class MessageLogPage : ContentPage
{
    string _contactId;
    Editor _messageDisplay;
    Entry _inputField;

    public MessageLogPage(string contactId)
    {
        _contactId = contactId; 
        Title = $"Vestlus: {contactId}";

        _messageDisplay = new Editor { IsReadOnly = true, FontSize = 14 };
        _inputField = new Entry { Placeholder = "Kirjuta sõnum...", HorizontalOptions = LayoutOptions.FillAndExpand };

        var sendBtn = new Button { Text = "Saada" };
        sendBtn.Clicked += OnSendClicked;

        string savedHistory = Preferences.Get(_contactId, "");
        _messageDisplay.Text = string.IsNullOrWhiteSpace(savedHistory) ? "Sõnumid puuduvad..." : savedHistory;

        var backBtn = new Button
        {
            Text = "Tagasi kontaktide juurde",
            BackgroundColor = Colors.Gray,
            TextColor = Colors.White,
            Margin = new Thickness(0, 10, 0, 0)
        };

        backBtn.Clicked += async (s, e) =>
        {
            await Navigation.PushAsync(new Phone());
        };

        var emailBtn = new Button { Text = "Vali ja Saada Email", BackgroundColor = Colors.Orange };
        emailBtn.Clicked += Saada_Valitud_Sõnum_Email_Clicked;

        var smsBtn = new Button
        {
            Text = "Vali ja Saada SMS",
            BackgroundColor = Colors.Green 
        };
        smsBtn.Clicked += Saada_Valitud_Sõnum_SMS_Clicked;

        var christmasBtn = new Button { Text = "Saada Jõulusõnum", BackgroundColor = Colors.Red, TextColor = Colors.White };
        christmasBtn.Clicked += Saada_Joulusõnum_Clicked;

        var birthdayBtn = new Button { Text = "Saada Sünnipäeva sõnum", BackgroundColor = Colors.Yellow, TextColor = Colors.White };
        birthdayBtn.Clicked += Saada_Sünnipäevasõnum_Clicked;

        var funeralBtn = new Button { Text = "Saada matuse sõnum", BackgroundColor = Colors.Black, TextColor = Colors.White };
        funeralBtn.Clicked += Saada_Matusesõnum_Clicked;

        var greetingBtn = new Button { Text = "Saada tervitus sõnum", BackgroundColor = Colors.Red, TextColor = Colors.White };
        greetingBtn.Clicked += Saada_Tervitusesõnum_Clicked;

        var bombBtn = new Button { Text = "Saada pommiähvardus", BackgroundColor = Colors.Orange, TextColor = Colors.White };
        bombBtn.Clicked += Saada_Pommiähvardussõnum_Clicked;


        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10, 
            Children = {
            new ScrollView { Content = _messageDisplay, HeightRequest = 300 },
            new HorizontalStackLayout { Spacing = 5, Children = { _inputField, sendBtn } },
            backBtn,
            emailBtn,
            smsBtn,

            new Label { Text = "Kiirvalikud:", Margin = new Thickness(0,10,0,0) },
            christmasBtn,
            birthdayBtn,
            funeralBtn,
            greetingBtn,
            bombBtn

        }
        };
    }

    void OnSendClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_inputField.Text)) return;

        string timestamp = DateTime.Now.ToString("HH:mm");
        string currentHistory = _messageDisplay.Text;
        string newLog = _messageDisplay.Text + $"\n[{timestamp}] Mina: {_inputField.Text}";

        _messageDisplay.Text = newLog;
        _inputField.Text = "";

        Preferences.Set(_contactId, newLog);
    }

    private async Task ExecuteEmail(string recipient, string body)
    {
        if (string.IsNullOrWhiteSpace(recipient))
        {
            await DisplayAlert("Viga", "E-posti aadress puudub!", "OK");
            return;
        }

        var message = new EmailMessage
        {
            Subject = "Teemaline sõnum!",
            Body = body,
            To = new List<string> { recipient }
        };

        if (Email.Default.IsComposeSupported)
        {
            await Email.Default.ComposeAsync(message);
        }
        else
        {
            await DisplayAlert("Viga", "E-posti saatmine ei ole selles seadmes toetatud.", "OK");
        }
    }


    private async void Saada_Valitud_Sõnum_Email_Clicked(object sender, EventArgs e)
    {
        string fullText = _messageDisplay.Text;
        var lines = fullText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Where(l => !l.Contains("Sõnumid puuduvad")).ToArray();

        if (lines.Length == 0) return;

        string selection = await DisplayActionSheet("Vali e-mail mida saata:", "Tühista", null, lines.Reverse().Take(5).ToArray());

        if (selection != "Tühista" && selection != null)
        {
            var e_mail = new EmailMessage { Subject = "Edastatud sõnum", Body = selection, To = new List<string> { _contactId } };
            if (Email.Default.IsComposeSupported)
                await Email.Default.ComposeAsync(e_mail);
        }
    }

    private async void Saada_Valitud_Sõnum_SMS_Clicked(object sender, EventArgs e)
    {
        string fullText = _messageDisplay.Text;
        var lines = fullText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Where(l => !l.Contains("Sõnumid puuduvad")).ToArray();

        if (lines.Length == 0) return;

        string selection = await DisplayActionSheet("Vali SMS mida saata:", "Tühista", null, lines.Reverse().Take(5).ToArray());

        if (selection != "Tühista" && selection != null)
        {

            var sms = new SmsMessage(selection, _contactId);

            if (Sms.Default.IsComposeSupported)
            {
                await Sms.Default.ComposeAsync(sms);
            }
            else
            {
                await DisplayAlert("Viga", "Sõnumite saatmine ei ole selles seadmes toetatud.", "OK");
            }
        }
    }

    private async void Saada_Joulusõnum_Clicked(object sender, EventArgs e)
    {
      
        var msgs = new List<string>
    {
        "Häid jõule!  Soovin sulle rahulikku pühadeaega!",
        "Kauneid jõule ja meeleolukat vana aasta lõppu!",
        "Soojust südamesse ja palju kingitusi kuuse alla!",
        "Rõõmsaid pühi sulle ja sinu lähedastele!"
    };

       
        Random rnd = new Random();
        int index = rnd.Next(msgs.Count);
        string randomMessage = msgs[index];

  
        await ExecuteEmail(_contactId, randomMessage);
    }
    private async void Saada_Sünnipäevasõnum_Clicked(object sender, EventArgs e)
    {

        var msgs = new List<string>
    {
        "Palju õnne! Olgu su päev õnnistatud rõõmuga!",
        "Palju õnne sünnipäevaks! Järjekordne päev kus kasvad ülese!",
        "Palju palju õnne sulle! Ära unusta naeratada!!",
        "Happy birthday!! olgu su päev äge!"
    };


        Random rnd = new Random();
        int index = rnd.Next(msgs.Count);
        string randomMessage = msgs[index];

        await ExecuteEmail(_contactId, randomMessage);
    }

    private async void Saada_Matusesõnum_Clicked(object sender, EventArgs e)
    {

        var msgs = new List<string>
    {
        "Tunnen sulle kaasa! Loodan sulle parimat",
        "Ära kurvasta! Küll sa leiad endas rahu",
        "Ma loodan sul läheb hästi! Naerata!",
        "Ma olen sulle abiks. Helista mulle!"
    };


        Random rnd = new Random();
        int index = rnd.Next(msgs.Count);
        string randomMessage = msgs[index];

        await ExecuteEmail(_contactId, randomMessage);
    }

    private async void Saada_Tervitusesõnum_Clicked(object sender, EventArgs e)
    {

        var msgs = new List<string>
    {
        "Tere! tore tutvuda sinuga",
        "Hei! saame tutavaks",
        "Tervist! kuidas sul läheb?",
        "Yo saame kokku"
    };


        Random rnd = new Random();
        int index = rnd.Next(msgs.Count);
        string randomMessage = msgs[index];

        await ExecuteEmail(_contactId, randomMessage);
    }

    private async void Saada_Pommiähvardussõnum_Clicked(object sender, EventArgs e)
    {

        var msgs = new List<string>
    {
        "Sinu postkastis on pipebomb",
        "Kanna raha või lenda õhku",
        "Sinu laen on maksmata, kanna raha või muidu...",
        "Droonid on liikvel, põgene"
    };


        Random rnd = new Random();
        int index = rnd.Next(msgs.Count);
        string randomMessage = msgs[index];

        await ExecuteEmail(_contactId, randomMessage);
    }










}