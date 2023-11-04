using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
using FlashCards.Domain.Entities.FlashCards;
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
        Task<bool> DeleteFlashCard(DeleteFlashCardRequest request);
        Task<List<FlashCardResponse>> GetFlashCards(GetFlashCardsRequest request);
        Task<List<FlashCardResponse>> GetFilteredFlashCards(GetFilteredFlashCardsRequest<FlashCard> request);
        Task<FlashCardResponse> UpdateFlashCard(UpdateFlashCardRequest request);
    }
}
