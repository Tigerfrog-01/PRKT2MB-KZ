namespace Projekt2_TARpe24_Kristopher;

public partial class TimerPage : ContentPage
{
    public TimerPage()
    {
        InitializeComponent();
        timer_btn.Text = "0"; // Initial state
    }

    bool on_off = false;
    int seconds = 0; // To track elapsed time

    private async void ShowTime()
    {
        while (on_off)
        {
            seconds++; // Increment by 1
            timer_btn.Text = seconds.ToString(); // Display the number

           

            await Task.Delay(1000);
        }
    }

    private void timer_btn_Clicked(object sender, EventArgs e)
    {
        if (on_off)
        {
           
            on_off = false;
            
            
        }
        else
        {
            // Start counting
            on_off = true;
            ShowTime();
        }
    }
    private async void tagasi_Clicked(object sender, EventArgs e)
    {
        on_off = false; 
        Navigation.PushAsync(new StartPage());
    }

}