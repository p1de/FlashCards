using FlashCards.Maui.ViewModels;

namespace FlashCards.Maui.Pages.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}