using FlashCards.Core.Application.CQRS.FlashCards.Common;
using MediatR;

namespace FlashCards.Core.Application.CQRS.FlashCards.Queries.Get
{
    public record GetFlashCardsQuery(
        string UserId,
        int Page,
        int Limit
    ) : IRequest<List<FlashCardResult>>;
}