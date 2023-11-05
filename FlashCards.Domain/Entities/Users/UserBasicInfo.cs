using FlashCards.Domain.Entities.Interfaces;

namespace FlashCards.Domain.Entities.Users
{
    public class UserBasicInfo : IIdentity
    {
        public string? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserBasicInfo() { }

        public UserBasicInfo(User user) 
        { 
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
        }
    }
}