
using Microsoft.Maui.Controls;
using System.Security.Cryptography;

namespace Projekt2_TARpe24_Kristopher;

public partial class DisasterRecipe : ContentPage
{
    Label Pealkiri;
    Label Kategooria;
    Label Image;

    private Entry OmadusEntry;
    private Entry KategooriEntry;
    private Entry imageUrlEntry;

  

    private Button lisaNupp;
    public DisasterRecipe()
    {
        InitializeComponent();



        Pealkiri = new Label
        {
            Text = "Retsepti nimi"
        };
        OmadusEntry = new Entry {Placeholder = ("nt Kirjukoer") };
        Kategooria = new Label
        {
            Text = "Retsepti kategooria"
        };
        KategooriEntry = new Entry {Placeholder = ("nt Magustoit") };
        Image = new Label
        {
            Text = "Retsepti pilt"
        };
        imageUrlEntry = new Entry {Placeholder = ("nt Kirjukoera pilt") };


        lisaNupp = new Button
        {
            Text = "Lisa Retsept",
            BackgroundColor = Colors.ForestGreen,
            TextColor = Colors.White,
            CornerRadius = 10
        };

        var mainStack = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 10,
            Children = { new BoxView { HeightRequest = 1, Color = Colors.Gray },Pealkiri,OmadusEntry,Kategooria,KategooriEntry,Image,imageUrlEntry, lisaNupp }
        };
        this.Content = mainStack;

    }



       
        

      
}