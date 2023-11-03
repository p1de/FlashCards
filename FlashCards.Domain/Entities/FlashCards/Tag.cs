using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }

        [ManyToMany(typeof(FlashCardTag))]
        public List<FlashCard>? FlashCards { get; set;}
    }
}