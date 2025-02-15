using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.ViewModels;

namespace PollingSystem.MAUI.Views;

public partial class VotePollPage : ContentPage
{
	public VotePollPage(Poll poll)
	{
		InitializeComponent();
        BindingContext = new VotePollViewModel(poll);
    }
}