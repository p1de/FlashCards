using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Update
{
    public record UpdateFlashCardCommand(
        string Id,
        string Word,
        string WordTranslation,
        string Description,
        List<Tag> Tags,
        bool IsShared
    ) : IRequest<FlashCardResult>;
}
