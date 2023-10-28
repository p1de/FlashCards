namespace FlashCards.Core.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string email, string password);

        AuthenticationResult Register(string username, string email, string password);
    }
}