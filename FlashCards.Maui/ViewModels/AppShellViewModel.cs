using CommunityToolkit.Mvvm.Input;
using FlashCards.Domain.Entities.Users;
using FlashCards.Maui.Pages.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Maui.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {
        [RelayCommand]
        private async Task SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
                App.UserDetails = new UserBasicInfo();
                App.Token = string.Empty;
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
