using FlashCards.Contracts.Authentication;
using FlashCards.Core.Application.Services.Authentication;
using FlashCards.Maui.Managers.Interfaces;

namespace FlashCards.Maui.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationManager(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public AuthenticationResponse Login(LoginRequest request)
        {
            var authenticationResult = _authenticationService.Login(
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authenticationResult.Id,
                authenticationResult.Username,
                authenticationResult.Email,
                authenticationResult.Token);

            return response;
        }

        public AuthenticationResponse Register(RegisterRequest request)
        {
            var authenticationResult = _authenticationService.Register(
                request.Username,
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authenticationResult.Id,
                authenticationResult.Username,
                authenticationResult.Email,
                authenticationResult.Token);

            return response;
        }
    }
}