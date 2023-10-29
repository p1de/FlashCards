using FlashCards.Maui.ViewModels.Dashboard;

namespace FlashCards.Maui.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashboardPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}