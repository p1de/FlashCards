using FlashCards.Domain.Entities.Users;
using MongoDB.Bson.Serialization.Attributes;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class FlashCard
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }

        [OneToOne("UserId")]
        public UserBasicInfo? User { get; set; }
        public string Word { get; set; } = string.Empty;
        public string WordTranslation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ManyToMany(typeof(FlashCardTag))]
        public List<Tag>? Tags { get; set; }
        public FlashCard? Clone() => MemberwiseClone() as FlashCard;
    }
}