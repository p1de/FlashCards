using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCards.Contracts.Authentication;
using FlashCards.Maui.Managers.Interfaces;
using FlashCards.Maui.Pages.Startup;

namespace FlashCards.Maui.ViewModels.Startup
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        private readonly IAuthenticationManager _authenticationManager;

        public RegisterPageViewModel(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [RelayCommand]
        private async Task Register()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var response = await _authenticationManager.Register(new RegisterRequest(Username, Email, Password));

                if (response.Token != string.Empty)
                {
                    await Shell.Current.DisplayAlert("Success", "Registered successfully", "OK");
                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else if (response.Username == string.Empty)
                {
                    await Shell.Current.DisplayAlert("Error", "User with given username already exists.", "OK");
                }
                else if (response.Email == string.Empty)
                {
                    await Shell.Current.DisplayAlert("Error", "User with given email already exists.", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning", "Some of the fields were left empty.", "OK");
            }
        }
    }
}