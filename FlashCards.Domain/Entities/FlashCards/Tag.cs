using FlashCards.Domain.Entities.Interfaces;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class Tag : IIdentity
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; } = Guid.Empty.ToString();
        public string? Name { get; set; }

        [ManyToMany(typeof(FlashCardTag))]
        public List<FlashCard>? FlashCards { get; set;}
    }
}