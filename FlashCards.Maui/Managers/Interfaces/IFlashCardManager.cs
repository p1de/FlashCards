using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
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
        Task<List<FlashCardResponse>> GetFlashCards(GetFlashCardsQuery request);
        Task<FlashCardResponse> ShareFlashCard(ShareFlashCardRequest request);
        Task<FlashCardResponse> UpdateFlashCard(UpdateFlashCardRequest request);
    }
}
