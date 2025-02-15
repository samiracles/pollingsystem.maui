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
        tabView.SelectionChanged += TabView_SelectionChanged;
    }

    private void TabView_SelectionChanged(object? sender, TabSelectionChangedEventArgs e)
    {

    }
}