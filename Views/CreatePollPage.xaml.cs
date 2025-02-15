using PollingSystem.MAUI.ViewModels;

namespace PollingSystem.MAUI.Views;

public partial class CreatePollPage : ContentPage
{
	public CreatePollPage()
	{
		InitializeComponent();
        BindingContext = new CreatePollViewModel();
    }
}