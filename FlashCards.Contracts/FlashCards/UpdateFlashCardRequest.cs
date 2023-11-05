using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Contracts.FlashCards
{
    public record UpdateFlashCardRequest(
        string Id,
        string UserId,
        string Word,
        string WordTranslation,
        string Description,
        List<string> TagsIds,
        bool IsShared
    );
}
