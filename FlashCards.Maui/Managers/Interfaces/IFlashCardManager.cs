using FlashCards.Contracts.FlashCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Maui.Managers.Interfaces
{
    public interface IFlashCardManager
    {
        Task<FlashCardResponse> CreateFlashCard(CreateFlashCardRequest request);
        Task<FlashCardResponse> ShareFlashCard(ShareFlashCardRequest request);
    }
}
