namespace Projekt2_TARpe24_Kristopher;

public partial class Phone : ContentPage
{
    EntryCell phoneCell;
    EntryCell emailCell;
    TableSection listSection;
    string defaultImage = "kalapulk1.png";

    public Phone()
    {
        BackgroundColor = Color.FromHex("#E8985E");

        InitializeComponent();

        phoneCell = new EntryCell { Label = "Telefon", Placeholder = "Sinu number", Keyboard = Keyboard.Telephone, LabelColor = Colors.White };
        emailCell = new EntryCell { Label = "Email", Placeholder = "Sinu email", Keyboard = Keyboard.Email, LabelColor = Colors.White };

        var saveButton = new Button
        {
            Text = "Lisa Märkmikku",
            BackgroundColor = Color.FromHex("#A9714B"),
            TextColor = Colors.White,
            CornerRadius = 10,
            HeightRequest = 45,
            Margin = new Thickness(20, 5, 20, 10),
            HorizontalOptions = LayoutOptions.Fill
        };
        saveButton.Clicked += OnSaveClicked;

        var inputSection = new TableSection("");

        inputSection.Add(new ViewCell
        {
            View = new VerticalStackLayout
            {
                BackgroundColor = Color.FromHex("#A9714B"),
                Padding = new Thickness(15, 10),
                HorizontalOptions = LayoutOptions.Fill, 
                Children = {
                    new Label { Text = "LISA UUS KONTAKT:", TextColor = Colors.White, FontAttributes = FontAttributes.Bold }
                }
            }
        });

        inputSection.Add(phoneCell);
        inputSection.Add(emailCell);
        inputSection.Add(new ViewCell { View = saveButton });

        listSection = new TableSection("");

        listSection.Add(new ViewCell
        {
            View = new VerticalStackLayout
            {
                BackgroundColor = Color.FromHex("#A9714B"),
                Padding = new Thickness(15, 0),
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Fill, 
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = "SALVESTATUD KONTAKTID:",
                        TextColor = Colors.White,
                        FontAttributes = FontAttributes.Bold,
                        VerticalTextAlignment = TextAlignment.Center, 
                        HeightRequest = 50
                    }
                }
            }
        });

        var myTable = new Microsoft.Maui.Controls.TableView
        {
            Intent = TableIntent.Form,
            BackgroundColor = Color.FromHex("#E8985E"), 
            Root = new TableRoot
            {
                inputSection,
                listSection
            }
        };

        this.Content = myTable;

     




        string savedData = Preferences.Get("ContactList", "");
        var contacts = savedData.Split('|', StringSplitOptions.RemoveEmptyEntries);
        foreach (var c in contacts)
        {
            var parts = c.Split(';');
            if (parts.Length >= 2)
            {
              
                string img = parts.Length >= 3 ? parts[2] : defaultImage;
                listSection.Add(CreateContactCell(parts[0], parts[1], img));
            }
        }
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string tel = phoneCell.Text;
        string email = emailCell.Text;

        if (string.IsNullOrWhiteSpace(tel)) return;

        string imagePath = defaultImage;
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Vali profiilipilt",
                FileTypes = FilePickerFileType.Images
            });
            if (result != null) imagePath = result.FullPath;
        }
        catch { }

        var newContact = CreateContactCell(tel, email, imagePath);
        listSection.Add(newContact);

        string currentList = Preferences.Get("ContactList", "");
        Preferences.Set("ContactList", currentList + $"{tel};{email};{imagePath}|");

        phoneCell.Text = "";
        emailCell.Text = "";
    }

    private ImageCell CreateContactCell(string tel, string email, string imagePath)
    {
        var cell = new ImageCell { Text = tel, Detail = email,ImageSource = imagePath, TextColor = Colors.White, DetailColor = Colors.White };
        cell.Tapped += async (s, args) =>
        {
            string display = string.IsNullOrWhiteSpace(email) ? tel : email;

            string action = await DisplayActionSheet(display, "Tagasi", null, "Sõnumid", "Kustuta");

            if (action == "Sõnumid")
            {
                string target = string.IsNullOrWhiteSpace(email) ? tel : email;
                await Navigation.PushAsync(new MessageLogPage(target));
            }
            else if (action == "Kustuta")
            {
                listSection.Remove(cell);
            
            }
        };
        return cell;
    }
}