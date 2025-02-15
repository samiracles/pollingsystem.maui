using PollingSystem.MAUI.ViewModels;
using Syncfusion.Maui.TabView;

namespace PollingSystem.MAUI.Views;

public partial class DashboardPage : ContentPage
{
    DashboardPageViewModel _viewModel;
    public DashboardPage()
	{
		InitializeComponent();
        BindingContext = _viewModel = new DashboardPageViewModel();    
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.RefreshCommand.Execute(null);
    }
}