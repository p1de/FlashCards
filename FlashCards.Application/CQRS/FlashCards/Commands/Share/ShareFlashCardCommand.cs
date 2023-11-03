using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using MediatR;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Share
{
    public record ShareFlashCardCommand(
        string UserId,
        string Word,
        string WordTranslation,
        string Description,
        List<Tag> Tags
    ) : IRequest<FlashCardResult>;
}
