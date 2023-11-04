using FlashCards.Domain.Entities.Interfaces;
using FlashCards.Domain.Entities.Users;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class FlashCard : IIdentity
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.Empty.ToString();
        public string? UserId { get; set; }

        [OneToOne("UserId")]
        public UserBasicInfo? User { get; set; }
        public string Word { get; set; } = string.Empty;
        public string WordTranslation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsShared { get; set; }

        [ManyToMany(typeof(FlashCardTag))]
        public List<Tag>? Tags { get; set; }
    }
}