
namespace Projekt2_TARpe24_Kristopher;

public partial class Lumememm : ContentPage
{
    public Lumememm()
    {
        InitializeComponent();

      
     

    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
    {
        ColorPanel.IsVisible = false;
        Joulud.IsVisible = false;

        double val = e.NewValue; 

      
        Paike.Opacity = val;
        Paike.Scale = 0.5 + (val * 0.5); 

        double snowmanOpacity = 1 - val;
        double snowmanScale = 1 - (val * 0.5); 

   
        HatSection.Opacity = snowmanOpacity;
        HatSection.Scale = snowmanScale;
        HatSection.TranslationY = val * 50; 

       
        HeadSection.Opacity = snowmanOpacity;
        HeadSection.Scale = snowmanScale;

        MidSection.Opacity = snowmanOpacity;
        MidSection.Scale = snowmanScale;

        BottomSection.Opacity = snowmanOpacity;
        BottomSection.Scale = snowmanScale;
    }

    private void KaotaAra(object sender, EventArgs e)
    {
        Joulud.IsVisible = false;


        Background.BackgroundColor = Colors.SkyBlue;
        ColorPanel.IsVisible = false;
        _ = Paike.FadeTo(1, 2000);

        Task.WhenAll(
           HatSection.FadeTo(0, 2000),
           HatSection.TranslateTo(0, 50, 2000),
           HeadSection.FadeTo(0, 2000),
           MidSection.FadeTo(0, 2000),
           BottomSection.FadeTo(0, 2000)
       );
    }

    private void NaitaNuud(object sender, EventArgs e)
    {
        Joulud.IsVisible = false;

        ColorPanel.IsVisible = false;

        _ = Paike.FadeTo(0, 0);
     

        Task.WhenAll(
        
        HatSection.FadeTo(1, 2000),
           HatSection.TranslateTo(1,1,2000),
           HeadSection.FadeTo(1, 2000),
           MidSection.FadeTo(1, 2000),
           BottomSection.FadeTo(1, 2000)
       );
    }

    private async void Varvi(object sender, EventArgs e)
    {

        bool answer =  await DisplayAlert("Kinnita", "Kas ikka soovid värvida lumememme?", "Jah", "Ei");

        if (answer == false) return;
        ColorPanel.IsVisible = false;
        Joulud.IsVisible = false;

        Random rand = new Random();
        Random rand1 = new Random();
        Random rand2 = new Random();
        Random rand3 = new Random();
        Random rand4 = new Random();

        int r = rand.Next(256);
        int g = rand.Next(256);
        int b = rand.Next(256);

        int r1 = rand1.Next(256);
        int g1 = rand1.Next(256);
        int b1 = rand1.Next(256);

        int r2 = rand2.Next(256);
        int g2 = rand2.Next(256);
        int b2 = rand2.Next(256);

        int r3 = rand3.Next(256);
        int g3 = rand3.Next(256);
        int b3 = rand3.Next(256);

        int r4 = rand4.Next(256);
        int g4 = rand4.Next(256);
        int b4 = rand4.Next(256);

        Color randomColorBody = Color.FromRgb(r, g, b);
        Color randomColorHat = Color.FromRgb(r1, g1, b1);
        Color randomColorEye = Color.FromRgb(r2, g2, b2);
        Color randomColorCarrot = Color.FromRgb(r3, g3, b3);
        Color randomColorButton = Color.FromRgb(r4, g4, b4);

        hat.BackgroundColor = randomColorBody;

        hat.BackgroundColor = randomColorHat;
        hat.BackgroundColor = randomColorEye;
        hat.BackgroundColor = randomColorCarrot;
        hat.BackgroundColor = randomColorButton;

        carrot.Fill = randomColorCarrot;

        eye1.Fill = randomColorEye;

        eye2.Fill = randomColorEye;

        button1.Fill = randomColorButton;

        button2.Fill = randomColorButton;

        button3.Fill = randomColorButton;

        button4.Fill = randomColorButton;

        button5.Fill = randomColorButton;

        head.Fill = randomColorBody;

        upperbody.Fill = randomColorBody;

        lowerbody.Fill = randomColorBody;

    }

    private void ValiVarv(object sender, EventArgs e)
    {
        ColorPanel.IsVisible = true;
        Joulud.IsVisible = false;
    }

    private void OnColorSliderChanged(object sender, EventArgs e)
    {
        Joulud.IsVisible = false;


        int r = (int)redSlider.Value;
        int g = (int)greenSlider.Value;
        int b = (int)blueSlider.Value;
 
        Color color = Color.FromRgb(r, g, b);

        head.Fill = color;
        upperbody.Fill = color;
        lowerbody.Fill = color;

    }

    private async void Tantsinuud(object sender, EventArgs e)
    {
        Joulud.IsVisible = false;
        ColorPanel.IsVisible = false;
        for (int i = 0; i < 5; i++)
        {
            await Task.WhenAll(

              head.TranslateTo(-20, 10, 200),
              eye1.TranslateTo(-20, 10, 200),
              eye2.TranslateTo(-20, 10, 200),
              upperbody.TranslateTo(-20, 10, 200),
              button1.TranslateTo(-20, 10, 200),
              button2.TranslateTo(-20, 10, 200),
              button3.TranslateTo(-20, 10, 200),
              button4.TranslateTo(-20, 10, 200),
              button5.TranslateTo(-20, 10, 200),
              carrot.TranslateTo(-20, 10, 200),
              hat.TranslateTo(-20, 10, 200),
              lowerbody.TranslateTo(-20, 10, 200)

          );

            await Task.WhenAll(

                head.TranslateTo(0, 0, 200),
                upperbody.TranslateTo(0, 0, 200),
                 eye1.TranslateTo(0, 0, 200),
              eye2.TranslateTo(0, 0, 200),
                button1.TranslateTo(0, 0, 200),
                button2.TranslateTo(0, 0, 200),
                button3.TranslateTo(0, 0, 200),
                button4.TranslateTo(0, 0, 200),
                button5.TranslateTo(0, 0, 200),
                carrot.TranslateTo(0, 0, 200),
                 hat.TranslateTo(0, 0, 200),
                lowerbody.TranslateTo(0, 0, 200)
            );

            await Task.WhenAll(

            head.TranslateTo(20, 10, 200),
            upperbody.TranslateTo(20, 10, 200),
            eye1.TranslateTo(20, 10, 200),
              eye2.TranslateTo(20, 10, 200),
              button1.TranslateTo(20, 10, 200),
              button2.TranslateTo(20, 10, 200),
              button3.TranslateTo(20, 10, 200),
              button4.TranslateTo(20, 10, 200),
              button5.TranslateTo(20, 10, 200),
              carrot.TranslateTo(20, 10, 200),
              hat.TranslateTo(20, 10, 200),
              lowerbody.TranslateTo(20, 10, 200)
        );

            await Task.WhenAll(

                 head.TranslateTo(0, 0, 200),
                upperbody.TranslateTo(0, 0, 200),
                   eye1.TranslateTo(0, 0, 200),
              eye2.TranslateTo(0, 0, 200),
                button1.TranslateTo(0, 0, 200),
                button2.TranslateTo(0, 0, 200),
                button3.TranslateTo(0, 0, 200),
                button4.TranslateTo(0, 0, 200),
                button5.TranslateTo(0, 0, 200),
                carrot.TranslateTo(0, 0, 200),
                 hat.TranslateTo(0, 0, 200),
                lowerbody.TranslateTo(0, 0, 200)
            );






        }

     
        head.Rotation = 0;
        upperbody.Rotation = 0;
    }

    public void   Oonuud(object sender, EventArgs e)
    {
        Joulud.IsVisible = false;
        ColorPanel.IsVisible = false;
        Background.BackgroundColor = Colors.DarkSlateBlue;
    }

    public void Tekstnuud(object sender, EventArgs e)
    {
        Joulud.IsVisible = true;
        ColorPanel.IsVisible = false;

    }



}

   
      



