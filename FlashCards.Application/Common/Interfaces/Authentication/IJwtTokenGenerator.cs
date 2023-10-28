namespace FlashCards.Core.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(Guid userId, string username);
    }
}