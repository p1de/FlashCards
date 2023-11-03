using FlashCards.Core.Application.Common.Interfaces.Services;
using FlashCards.Domain.Entities.Users;
using FlashCards.Infrastructure.Services;
using FlashCards.Maui.Pages.Dashboard;
using FlashCards.Maui.Pages.Startup;
using FlashCards.Maui.Views;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace FlashCards.Maui.ViewModels.Startup
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public LoadingPageViewModel(IDateTimeProvider dateTimeProvider) 
        {
            _dateTimeProvider = dateTimeProvider;
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");
            if (string.IsNullOrEmpty(userDetailsStr))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                var tokenDetails = await SecureStorage.GetAsync(nameof(App.Token));

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenDetails) as JwtSecurityToken;

                if (jsonToken.ValidTo < _dateTimeProvider.UtcNow)
                {
                    await Shell.Current.DisplayAlert("User session expired.", "Sign In again to continue.", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                App.Token = tokenDetails;
                Shell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
        }
    }
}
