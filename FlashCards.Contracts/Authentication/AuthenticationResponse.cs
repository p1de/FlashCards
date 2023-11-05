namespace FlashCards.Contracts.Authentication
{
    public record AuthenticationResponse(
        string Id,
        string Username,
        string Email,
        string Token
    );
}