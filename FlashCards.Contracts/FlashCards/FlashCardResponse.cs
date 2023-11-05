namespace FlashCards.Contracts.FlashCards
{
    public record FlashCardResponse(
        string Id,
        string UserId,
        string Username,
        string Word,
        string WordTranslation,
        string Description,
        List<string> TagsIds,
        bool IsShared = false);
}