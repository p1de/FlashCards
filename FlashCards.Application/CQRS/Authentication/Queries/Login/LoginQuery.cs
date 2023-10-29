using FlashCards.Core.Application.CQRS.Authentication.Common;
using MediatR;

namespace FlashCards.Core.Application.CQRS.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<AuthenticationResult>;
}
