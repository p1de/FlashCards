using FlashCards.Maui.ViewModels.Dashboard;

namespace FlashCards.Maui.Pages.Dashboard;

public partial class DashboardPage : ContentPage
{
    DashboardPageViewModel _viewModel;
    public DashboardPage(DashboardPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        _viewModel.Page = 0;
        base.OnAppearing();
        await _viewModel.LoadFlashCardsAsync();
    }
}