using FlashCards.Contracts.Authentication;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using FlashCards.Maui.Managers.Interfaces;
using System.Net;

namespace FlashCards.Maui;

public partial class MainPage : ContentPage
{
	IAuthenticationManager _authenticationManager;

    public MainPage(IAuthenticationManager authenticationManager)
	{
		_authenticationManager = authenticationManager;
		InitializeComponent();
	}

	private void OnRegisterButtonClicked(object sender, EventArgs e)
	{
		_authenticationManager.Register(new RegisterRequest(username.Text, email.Text, password.Text));
	}
}

