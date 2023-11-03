namespace FlashCards.Contracts.FlashCards
{
    public record FlashCardResponse(
        string Id,
        string UserId,
        string Word,
        string WordTranslation,
        string Description,
        List<string> TagsIds);
}