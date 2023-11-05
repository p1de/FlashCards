using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;

namespace FlashCards.Core.Application.CQRS.FlashCards.Common
{
    public class FlashCardResult
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public UserBasicInfo User {  get; set; }
        public string Word { get; set; }
        public string WordTranslation { get; set; }
        public string Description { get; set; }
        public List<Tag> Tags { get; set; }
        public bool IsShared { get; set; }

        public FlashCardResult()
        {
        }

        public FlashCardResult(FlashCard flashCard)
        {
            Id = flashCard.Id;
            UserId = flashCard.UserId;
            User = flashCard.User;
            Word = flashCard.Word;
            WordTranslation = flashCard.WordTranslation;
            Description = flashCard.Description;
            Tags = flashCard.Tags;
            IsShared = flashCard.IsShared;
        }
    }
}
