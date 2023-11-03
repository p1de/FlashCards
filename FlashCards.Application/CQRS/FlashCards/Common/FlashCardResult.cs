using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Core.Application.CQRS.FlashCards.Common
{
    public record FlashCardResult(
        string Id,
        string UserId,
        UserBasicInfo User,
        string Word,
        string WordTranslation,
        string Description,
        List<Tag> Tags
    );
}
