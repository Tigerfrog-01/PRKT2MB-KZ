using Microsoft.Extensions.DependencyInjection;

namespace Projekt2_TARpe24_Kristopher
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
       

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}