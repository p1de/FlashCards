using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlashCards.Contracts.Authentication;
using FlashCards.Domain.Entities.Users;
using FlashCards.Maui.Managers.Interfaces;
using FlashCards.Maui.Pages.Dashboard;
using FlashCards.Maui.Views;
using Newtonsoft.Json;

namespace FlashCards.Maui.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        private readonly IAuthenticationManager _authenticationManager;

        public LoginPageViewModel(IAuthenticationManager authenticationManager) 
        {
            _authenticationManager = authenticationManager;
        }

        [RelayCommand]
        async Task Login()
        {
            if (!string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password))
            {
                var response = _authenticationManager.Login(new LoginRequest(Email, Password));

                if (response.Token != string.Empty)
                {
                    var userDetails = new UserBasicInfo()
                    {
                        Email = response.Email,
                        Username = response.Username
                    };

                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;
                    Shell.Current.FlyoutHeader = new FlyoutHeaderControl();

                    await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning", "Some of the fields were left empty.", "OK");
            }
        }
    }
}