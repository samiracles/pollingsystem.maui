using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.ViewModels;

namespace PollingSystem.MAUI.Views;

public partial class PollResultPage : ContentPage
{
	public PollResultPage(Poll poll)
	{
		InitializeComponent();
		BindingContext = new PollResultViewModel(poll);
    }
}