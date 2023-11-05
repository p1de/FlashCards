using System.Linq.Expressions;

namespace FlashCards.Contracts.FlashCards
{
    public record GetFilteredFlashCardsRequest<T>(
        Expression<Func<T, bool>> Predicate,
        int Page,
        int Limit
    );
}
