using FlashCards.Contracts.Authentication;
using FlashCards.Maui.Managers.Interfaces;
using FlashCards.Maui.ViewModels.Startup;

namespace FlashCards.Maui.Pages.Startup;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}