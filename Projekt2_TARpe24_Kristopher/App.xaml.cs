using Projekt2_TARpe24_Kristopher.Views;

namespace Projekt2_TARpe24_Kristopher
{
    public partial class App : Application
    {
        private readonly MainTabbedPage mainTabbedPage;

        public App(MainTabbedPage mainTabbedPage)
        {
            InitializeComponent();
            this.mainTabbedPage = mainTabbedPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(mainTabbedPage);
        }
    }
}
