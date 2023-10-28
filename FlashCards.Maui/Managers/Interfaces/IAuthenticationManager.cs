using FlashCards.Contracts.Authentication;

namespace FlashCards.Maui.Managers.Interfaces
{
    public interface IAuthenticationManager
    {
        AuthenticationResponse Login(LoginRequest request);
        AuthenticationResponse Register(RegisterRequest request);
    }
}