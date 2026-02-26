using static Android.Graphics.ImageDecoder;
using static Java.Util.Jar.Attributes;

namespace Projekt2_TARpe24_Kristopher;

public partial class Lumememm : ContentPage
{
	public Lumememm()
	{
		InitializeComponent();

		Melt.Clicked += async (s, e) =>
		{

			Grid2.FadeTo(0, 2000, Easing.Linear);
            Grid3.FadeTo(0, 2000, Easing.Linear);
            Grid4.FadeTo(0, 2000, Easing.Linear);


            await Paike.FadeTo(1, 2000, Easing.CubicIn);


        };


}




	}
	
	
	
	
	


	