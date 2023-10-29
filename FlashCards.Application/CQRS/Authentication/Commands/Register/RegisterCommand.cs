using FlashCards.Core.Application.CQRS.Authentication.Common;
using MediatR;

namespace FlashCards.Core.Application.CQRS.Authentication.Commands.Register
{
    public record RegisterCommand(
        string Username,
        string Email,
        string Password) : IRequest<AuthenticationResult>;
}
