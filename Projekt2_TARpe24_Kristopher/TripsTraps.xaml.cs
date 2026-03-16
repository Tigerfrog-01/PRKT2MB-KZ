
using Microsoft.Maui.Graphics.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Projekt2_TARpe24_Kristopher;

public partial class TripsTraps : ContentPage
{

    Grid gr3x3;
    int[,] board = new int[3, 3];

    BoxView[,] kasteMaatriks = new BoxView[3, 3];
    Label KesOnKes;
    Label pealkiri;

 

  

    int turn = 1;
    int moveCount = 0;
    bool gameOver = false;

    public TripsTraps()
    {
    
            InitializeComponent();

     



        BackgroundColor = Colors.White;
        gr3x3 = Täida_gr3x3();
        gr3x3.Padding = 10;

        Button tagasiNupp = new Button
        {
            Text = "Tagasi Menüüsse",
            FontSize = 20,
            BackgroundColor = Colors.Beige,
            TextColor = Colors.Chocolate,
            CornerRadius = 10,
            HeightRequest = 50,
            WidthRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 0)
        };
        tagasiNupp.Clicked += async (s, e) =>
        {
            await Navigation.PushAsync(new TripsPea());
        };

   
        MainStack.Children.Add(tagasiNupp);

        pealkiri = new Label
        {
            Text = "Trips Traps Trull",
            FontSize = 32,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 20, 0, 20)
        };
        KesOnKes = new Label
        {
            Text = null,
            HorizontalOptions = LayoutOptions.Center,
            FontSize = 20
        };
        MainStack.Children.Add(pealkiri);
        MainStack.Children.Add(KesOnKes);
        MainStack.Children.Add(gr3x3);


    }

    public void ApplyTheme(Color backColor, Color textColor)
    {
        this.BackgroundColor = backColor;
        if (pealkiri != null) pealkiri.TextColor = textColor;
        if (KesOnKes != null) KesOnKes.TextColor = textColor;

     
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[r, c] == 0) 
                {
                    kasteMaatriks[r, c].BackgroundColor = (backColor == Colors.White) ? Colors.Gray : Colors.White;
                }
            }
        }
    }

    private Grid Täida_gr3x3()
    {

        
        Grid tempGrid = new Grid();

       
        tempGrid.HorizontalOptions = LayoutOptions.Center;
        tempGrid.VerticalOptions = LayoutOptions.Center;

        for (int i = 0; i < 3; i++)
        {
            tempGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            tempGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        }

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                 BoxView kast = new BoxView
                {
                    BackgroundColor = Colors.Gray,
                    Margin = 5,
                    CornerRadius = 10,
                    WidthRequest = 70,
                    HeightRequest = 70,
                };

                kasteMaatriks[r, c] = kast;

                int rida = r;
                int veerg = c;

                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += async (s, args) =>
                {
                    if (gameOver || board[rida, veerg] != 0) return;

                    board[rida, veerg] = turn;
                    kast.BackgroundColor = (turn == 1) ? Colors.Red : Colors.Blue;
                    moveCount++;
                    

                    if (CheckForWin())
                    {
                        gameOver = true;
                        await DisplayAlert("Võit!", $"Mängija {turn} võitis!", "Uus mäng");
                        ResetGame();
                        return;
                    }
                    else if (moveCount == 9)
                    {
                        await DisplayAlert("Viik!", "Mäng jäi viiki!", "Uus mäng");
                        ResetGame();
                        return;
                    }
                    else
                    {
                        turn = (turn == 1) ? 2 : 1;
                    }

                    if (turn == 1)
                    {
                        KesOnKes.Text = "Mängija 1 kord (Punane)";
                    }
                 
                    else if(turn == 2)
                    {
                        KesOnKes.Text = "Mängija 2 kord (Sinine)";
                    }
             

                 
                };

                kast.GestureRecognizers.Add(tap);
                tempGrid.Add(kast, c, r);
            }
        }
        return tempGrid;
    }

    private bool CheckForWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return true;
            if (board[0, i] != 0 && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return true;
        }
        if (board[0, 0] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return true;
        if (board[0, 2] != 0 && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return true;
        
        return false;
    }

    private void ResetGame()
    {
        Array.Clear(board, 0, board.Length);
        turn = 1;
        moveCount = 0;
        gameOver = false;
        KesOnKes.Text = null;


        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                kasteMaatriks[r, c].BackgroundColor = Colors.Gray;
            }
        }
    }
    private async void DarkMode(object sender, EventArgs e)
    {
        BackgroundColor = Color.FromHex("#2C4251");
        KesOnKes.TextColor = Colors.White;
        pealkiri.TextColor = Colors.White;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
          
                if (board[r, c] == 0)
                {
                    kasteMaatriks[r, c].BackgroundColor = Colors.White;
                }
            }
        }
    }
    private async void LightMode(object sender, EventArgs e)
    {
        BackgroundColor = Colors.White;
        KesOnKes.TextColor = Colors.Black;
        pealkiri.TextColor = Colors.Black;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
               
                if (board[r, c] == 0)
                {
                    kasteMaatriks[r, c].BackgroundColor = Colors.Gray;
                }
            }
        }
      

    }








}