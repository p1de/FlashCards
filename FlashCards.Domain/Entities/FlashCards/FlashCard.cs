using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class FlashCard
    {
        public FlashCard(string id, int userId, string name, string description, List<string> tags)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            Tags = tags;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public FlashCard? Clone() => MemberwiseClone() as FlashCard;
    }
}