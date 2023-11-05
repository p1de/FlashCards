using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Create
{
    public record CreateFlashCardCommand(
        string UserId,
        string Word,
        string WordTranslation,
        string Description,
        List<Tag> Tags
    ) : IRequest<FlashCardResult>;
}
