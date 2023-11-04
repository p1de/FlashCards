using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Contracts.FlashCards
{
    public record GetFlashCardsRequest(
        string UserId,
        int Page,
        int Limit
    );
}
