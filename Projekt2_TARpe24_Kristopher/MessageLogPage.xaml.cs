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

        var emailBtn = new Button { Text = "Vali ja Saada", BackgroundColor = Colors.Orange };
        emailBtn.Clicked += Saada_Valitud_Sõnum_Clicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10, 
            Children = {
            new ScrollView { Content = _messageDisplay, HeightRequest = 300 },
            new HorizontalStackLayout { Spacing = 5, Children = { _inputField, sendBtn } },
            backBtn,
            emailBtn
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

    private async void Saada_email_Clicked(object sender, EventArgs e)
    {

     
        string recipient = _contactId;

    
        string fullText = _messageDisplay.Text;
        if (string.IsNullOrWhiteSpace(fullText) || fullText == "Sõnumid puuduvad...")
        {
            await DisplayAlert("Viga", "Sõnumite ajalugu on tühi!", "OK");
            return;
        }

       
        string[] lines = fullText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        string lastMessage = lines.Last(); 

    
        var e_mail = new EmailMessage
        {
            Subject = "Sõnum märkmikust",
            Body = lastMessage,
            To = new List<string> { recipient }
        };

    
        if (Email.Default.IsComposeSupported)
        {
            await Email.Default.ComposeAsync(e_mail);
        }
        else
        {
            await DisplayAlert("Viga", "E-posti rakendust ei leitud", "OK");
        }
    }

    private async void Saada_Valitud_Sõnum_Clicked(object sender, EventArgs e)
    {
        string fullText = _messageDisplay.Text;
       
        var lines = fullText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                            .Where(l => !l.Contains("Sõnumid puuduvad"))
                            .ToArray();

        if (lines.Length == 0) return;

       
        string selection = await DisplayActionSheet("Vali sõnum mida saata:", "Tühista", null, lines.Reverse().Take(5).ToArray());

        if (selection != "Tühista" && selection != null)
        {
            var e_mail = new EmailMessage
            {
                Subject = "Edastatud sõnum",
                Body = selection,
                To = new List<string> { _contactId }
            };

            if (Email.Default.IsComposeSupported)
                await Email.Default.ComposeAsync(e_mail);
        }
    }


}