using FlashCards.Contracts.Authentication;
using FlashCards.Core.Application.CQRS.Authentication.Commands.Register;
using FlashCards.Core.Application.CQRS.Authentication.Queries.Login;
using FlashCards.Maui.Managers.Interfaces;
using MediatR;

namespace FlashCards.Maui.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IMediator _mediator;

        public AuthenticationManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var authenticationResult = await _mediator.Send(query);

            var response = new AuthenticationResponse(
                authenticationResult.Id,
                authenticationResult.Username,
                authenticationResult.Email,
                authenticationResult.Token);

            return response;
        }

        public async Task<AuthenticationResponse> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.Username, request.Email, request.Password);
            var authenticationResult = await _mediator.Send(command);

            var response = new AuthenticationResponse(
                authenticationResult.Id,
                authenticationResult.Username,
                authenticationResult.Email,
                authenticationResult.Token);

            return response;
        }
    }
}