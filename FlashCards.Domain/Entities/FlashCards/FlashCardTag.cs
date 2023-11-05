using FlashCards.Domain.Entities.Interfaces;
using SQLiteNetExtensions.Attributes;

namespace FlashCards.Domain.Entities.FlashCards
{
    public class FlashCardTag
    {
        [ForeignKey(typeof(FlashCard))]
        public string? FlashCardId { get; set; }

        [ForeignKey(typeof(Tag))]
        public string? TagId { get; set; }
    }
}