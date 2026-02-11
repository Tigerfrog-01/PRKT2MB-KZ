using Android.Telecom;
using AndroidX.ConstraintLayout.Utils.Widget;
using Microsoft.Maui.Controls.Shapes;

namespace Projekt2_TARpe24_Kristopher;

public partial class ValgusFloorPage : ContentPage
{
   
    Ellipse RedView;
    Ellipse YellowView;
    Ellipse GreenView;
    int currentState = 0;




    public ValgusFloorPage()
	{	
		InitializeComponent();

        var frame = new BoxView
        {
            Color = Color.FromHex("#989898"),
            WidthRequest = 200,
            HeightRequest = 500,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0), 
            CornerRadius = 30,
        };

        RedView = CreateLight(Colors.Red);
        YellowView = CreateLight(Color.FromHex("554400"));
        GreenView = CreateLight(Color.FromHex("004400"));

        var lightsStack = new VerticalStackLayout
        {
            Spacing = 15,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Children = {RedView,YellowView,GreenView}
        };
        var actionButton = new Button
        {
            Text = "Muuda värvi",
            Margin = new Thickness(0, 20, 0, 0),
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.DarkSlateGray,
            TextColor = Colors.White
        };
        actionButton.Clicked += OnChangeLightClicked;

        var mainGrid = new Grid
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };
        mainGrid.Children.Add(frame);
        mainGrid.Children.Add(lightsStack);

        var layoutWrapper = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = 20,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Children = { mainGrid, actionButton } 
        };
        this.Content = layoutWrapper;

       

    }

    private Ellipse CreateLight(Color color)
    {
        return new Ellipse
        {
            Fill = color,
            WidthRequest = 90,
            HeightRequest = 90,
            Stroke = Colors.Black,
            StrokeThickness = 2

        };




    }



    private void OnChangeLightClicked(object sender, EventArgs e)
    {
      
        currentState = (currentState + 1) % 3;

       
        RedView.Fill = Color.FromHex("#440000");
        YellowView.Fill = Color.FromHex("#444400");
        GreenView.Fill = Color.FromHex("#004400");

       
        switch (currentState)
        {
            case 0:
                RedView.Fill = Colors.Red;
                break;
            case 1:
                YellowView.Fill = Colors.Yellow;
                break;
            case 2:
                GreenView.Fill = Colors.Lime;
                break;
        }
    }



}