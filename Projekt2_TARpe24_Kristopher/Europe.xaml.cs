using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Projekt2_TARpe24_Kristopher;

public partial class Europe : ContentPage
{
    public ObservableCollection<Riik> riigid { get; set; }
    ListView list;

 
    Entry nimiEntry, pealinnEntry, rahvaarvEntry;
    string valitudPildiRada = "dotnet_bot.png"; 

    public Europe()
    {
        InitializeComponent();

        riigid = new ObservableCollection<Riik>
        {
            new Riik { Nimi="Eesti", Pealinn="Tallinn", Rahvaarv=1331000, Lipp="estonia.png" },
            new Riik { Nimi="Saksamaa", Pealinn="Berliin", Rahvaarv=83200000, Lipp="germany.png" }
        };

       
        nimiEntry = new Entry { Placeholder = "Riigi nimi" };
        pealinnEntry = new Entry { Placeholder = "Pealinn" };
        rahvaarvEntry = new Entry { Placeholder = "Rahvaarv", Keyboard = Keyboard.Numeric };

   
        Button btnValiPilt = new Button
        {
            Text = "Vali lipp galeriist",
            BackgroundColor = Colors.Blue,
            TextColor = Colors.White,
            Margin = new Thickness(0, 5)
        };
        btnValiPilt.Clicked += ValiPilt_Clicked;

        Button btnLisa = new Button
        {
            Text = "Lisa riik nimekirja",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White,
            Margin = new Thickness(0, 5)
        };
        btnLisa.Clicked += LisaNupp_Clicked;

        Button btnKustuta = new Button
        {
            Text = "Kustuta valitud",
            BackgroundColor = Colors.Red,
            TextColor = Colors.White,
            Margin = new Thickness(0, 5)
        };
        btnKustuta.Clicked += Kustuta_Clicked;

     
        list = new ListView
        {
            HasUnevenRows = true,
            ItemsSource = riigid,
            Margin = new Thickness(0, 10)
        };
        list.ItemTapped += List_ItemTapped;

        list.ItemTemplate = new DataTemplate(() =>
        {
            Image imgLipp = new Image { HeightRequest = 50, WidthRequest = 80, Aspect = Aspect.AspectFit };
            imgLipp.SetBinding(Image.SourceProperty, "Lipp");

            Label lblNimi = new Label { FontSize = 20, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
            lblNimi.SetBinding(Label.TextProperty, "Nimi");

            return new ViewCell
            {
                View = new StackLayout
                {
                    Padding = 10,
                    Orientation = StackOrientation.Horizontal,
                    Children = { imgLipp, lblNimi }
                }
            };
        });

        
        this.Content = new ScrollView 
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Children = {
                    new Label { Text = "Riikide haldus", FontSize = 22, HorizontalOptions = LayoutOptions.Center },
                    nimiEntry, pealinnEntry, rahvaarvEntry,
                    btnValiPilt, btnLisa, btnKustuta,
                    list
                }
            }
        };
    }



   
    private async void ValiPilt_Clicked(object sender, EventArgs e)
    {
        try
        {
            var pilt = await MediaPicker.Default.PickPhotoAsync();

            if (pilt != null)
            {
                
                valitudPildiRada = pilt.FullPath;
                await DisplayAlert("Korras", "Pilt on valitud!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Viga", $"Ei saanud galeriid avada: {ex.Message}", "OK");
        }
    }

 
    private void LisaNupp_Clicked(object sender, EventArgs e)
    {
        string nimi = nimiEntry.Text;
        if (string.IsNullOrWhiteSpace(nimi)) return;

        bool olemas = riigid.Any(r => r.Nimi.Equals(nimi, StringComparison.OrdinalIgnoreCase));

        if (olemas)
        {
            DisplayAlert("Viga", "See riik on juba nimekirjas!", "OK");
        }
        else
        {
            int arv = 0;
            int.TryParse(rahvaarvEntry.Text, out arv);

            riigid.Add(new Riik
            {
                Nimi = nimi,
                Pealinn = pealinnEntry.Text,
                Rahvaarv = arv,
                Lipp = valitudPildiRada 
            });

          
            nimiEntry.Text = pealinnEntry.Text = rahvaarvEntry.Text = "";
            valitudPildiRada = "dotnet_bot.png";
        }
    }

    private void Kustuta_Clicked(object sender, EventArgs e)
    {
        if (list.SelectedItem is Riik valitud)
        {
            riigid.Remove(valitud);
            list.SelectedItem = null;
        }
    }

    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Riik riik)
        {
            await DisplayAlert(riik.Nimi, $"Pealinn: {riik.Pealinn}\nRahvaarv: {riik.Rahvaarv:N0}", "OK");
            list.ItemsSource = null;
            list.ItemsSource = riigid;
        }
    }

    public class Riik
    {
        public string Nimi { get; set; }
        public string Pealinn { get; set; }
        public int Rahvaarv { get; set; }
        public string Lipp { get; set; } 
    }
}