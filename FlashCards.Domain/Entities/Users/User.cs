using FlashCards.Domain.Entities.Interfaces;
using SQLite;

namespace FlashCards.Domain.Entities.Users
{
    public class User : IIdentity
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.Empty.ToString();
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}