using FlashCards.Maui.ViewModels.Startup;

namespace FlashCards.Maui.Pages.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}