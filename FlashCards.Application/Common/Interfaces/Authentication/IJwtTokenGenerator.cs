namespace FlashCards.Core.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(string userId, string username);
    }
}