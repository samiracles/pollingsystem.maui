using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels;

namespace PollingSystem.MAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginViewModel();
    }
}