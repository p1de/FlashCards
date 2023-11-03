using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Contracts.FlashCards
{
    public record CreateFlashCardRequest(
        string Word,
        string WordTranslation,
        string Description,
        List<string> TagsIds
    );
}
