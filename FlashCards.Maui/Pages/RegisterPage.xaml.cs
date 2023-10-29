using FlashCards.Contracts.Authentication;
using FlashCards.Maui.Managers.Interfaces;

namespace FlashCards.Maui.Pages;

public partial class RegisterPage : ContentPage
{
    private IAuthenticationManager _authenticationManager;

    public RegisterPage(IAuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
        InitializeComponent();
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        _authenticationManager.Register(new RegisterRequest(username.Text, email.Text, password.Text));
    }
}