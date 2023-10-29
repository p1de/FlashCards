using FlashCards.Contracts.Authentication;

namespace FlashCards.Maui.Managers.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<AuthenticationResponse> Login(LoginRequest request);
        Task<AuthenticationResponse> Register(RegisterRequest request);
    }
}