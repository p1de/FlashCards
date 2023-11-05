namespace FlashCards.Core.Application.CQRS.Authentication.Common
{
    public record AuthenticationResult(
        string Id,
        string Username,
        string Email,
        string Token
    );
}