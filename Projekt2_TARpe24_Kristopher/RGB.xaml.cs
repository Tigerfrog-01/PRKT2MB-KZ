using Android.Content.Res;
using Android.Telecom;
using Microsoft.Maui.Controls;
using System.Security.Cryptography;
using static Android.Graphics.BlurMaskFilter;

namespace Projekt2_TARpe24_Kristopher;



public partial class RGB : ContentPage
{
    

    public RGB()
	{
		InitializeComponent();
 
       

    }
    void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (sender == redSlider)
        {
            redLabel.Text = String.Format("Red = {0:X2}", (int)args.NewValue);
        }
        else if (sender == greenSlider)
        {
            greenLabel.Text = String.Format("Green = {0:X2}", (int)args.NewValue);
        }
        else if (sender == blueSlider)
        {
            blueLabel.Text = String.Format("Blue = {0:X2}", (int)args.NewValue);
        }
        else if (sender == SizeSlider)
        {
            SizeLabel.Text = String.Format("Size = {0}", (int)args.NewValue);
        }
        else if (sender == SmoothSlider)
        {
            SmoothLabel.Text = String.Format("Smooth = {0}", (int)args.NewValue);
        }

         




        RedBox.Color = Color.FromRgb((int)redSlider.Value, 0, 0);
        GreenBox.Color = Color.FromRgb(0, (int)greenSlider.Value, 0);
        BlueBox.Color = Color.FromRgb(0, 0, (int)blueSlider.Value);

        RedBox.CornerRadius = (int)SmoothSlider.Value;
        GreenBox.CornerRadius = (int)SmoothSlider.Value;
        BlueBox.CornerRadius = (int)SmoothSlider.Value;



        boxView.Background = Color.FromRgb((int)redSlider.Value,
                                      (int)greenSlider.Value,
                                      (int)blueSlider.Value);

        boxView.HeightRequest = (int)SizeSlider.Value;
        boxView.WidthRequest = (int)SizeSlider.Value;

        boxView.CornerRadius = (int)SmoothSlider.Value;

        Animations.Clicked += async (s, e) =>
        {
            Random rand = new Random();

            int r = rand.Next(256);
            int g = rand.Next(256);
            int b = rand.Next(256);

            boxView.Background = Color.FromRgb(r, g, b);
            RedBox.Color = Color.FromRgb(r, 0, 0);
            GreenBox.Color = Color.FromRgb(0, g, 0);
            BlueBox.Color = Color.FromRgb(0, 0, b);

            redSlider.Value = r;
            greenSlider.Value = g;
            blueSlider.Value = b;


            redLabel.Text = String.Format("Red = {0:X2}", r);
            greenLabel.Text = String.Format("Green = {0:X2}", g);
            blueLabel.Text = String.Format("Blue = {0:X2}", b);



            

        };






    }

  




}