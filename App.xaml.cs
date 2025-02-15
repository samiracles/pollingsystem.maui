using PollingSystem.MAUI.Views;

namespace PollingSystem.MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new LoginPage();
        }
    }
}
