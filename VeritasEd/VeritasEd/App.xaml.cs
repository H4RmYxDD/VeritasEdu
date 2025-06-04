using VeritasEd.Views;
namespace VeritasEd
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage())
            {
                BarBackgroundColor = Colors.LightBlue,
                BarTextColor = Colors.White
            });
        }
    }
}