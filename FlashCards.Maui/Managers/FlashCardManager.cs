using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Commands.Create;
using FlashCards.Core.Application.CQRS.FlashCards.Commands.Update;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.GetFiltered;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Maui.Managers.Interfaces;
using MediatR;

namespace FlashCards.Maui.Managers
{
    public class FlashCardManager : IFlashCardManager
    {
        private readonly IMediator _mediator;

        public FlashCardManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<FlashCardResponse> CreateFlashCard(CreateFlashCardRequest request)
        {
            var command = new CreateFlashCardCommand(request.UserId, request.Word, request.WordTranslation, request.Description, new List<Tag>());

            var flashCardResult = await _mediator.Send(command);

            var response = new FlashCardResponse(
                flashCardResult.Id,
                flashCardResult.UserId,
                flashCardResult.User?.Username,
                flashCardResult.Word,
                flashCardResult.WordTranslation,
                flashCardResult.Description,
                flashCardResult.Tags?.Select(t => t.Id).ToList());

            return response;
        }

        public async Task<FlashCardResponse> UpdateFlashCard(UpdateFlashCardRequest request)
        {
            var command = new UpdateFlashCardCommand(request.Id, request.Word, request.WordTranslation, request.Description, new List<Tag>(), request.IsShared);

            var flashCardResult = await _mediator.Send(command);

            var response = new FlashCardResponse(
                flashCardResult.Id,
                flashCardResult.UserId,
                flashCardResult.Word,
                flashCardResult.WordTranslation,
                flashCardResult.Description,
                flashCardResult.User?.Username,
                flashCardResult.Tags?.Select(t => t.Id).ToList(), 
                flashCardResult.IsShared);

            return response;
        }

        public async Task<bool> DeleteFlashCard(DeleteFlashCardRequest request)
        {
            var command = new DeleteFlashCardCommand(request.Id);

            var result = await _mediator.Send(command);

            return result;
        }

        public async Task<List<FlashCardResponse>> GetFlashCards(GetFlashCardsRequest request)
        {
            var query = new GetFlashCardsQuery(request.UserId, request.Page, request.Limit);

            var flashCardResults = await _mediator.Send(query);

            var response = flashCardResults.Select(fcr 
                => new FlashCardResponse(
                    fcr.Id,
                    fcr.UserId,
                    fcr.User?.Username,
                    fcr.Word, 
                    fcr.WordTranslation, 
                    fcr.Description,
                    fcr.Tags.Select(t => t.Id).ToList(),
                    fcr.IsShared))
                .ToList();

            return response;
        }

        public async Task<List<FlashCardResponse>> GetFilteredFlashCards(GetFilteredFlashCardsRequest<FlashCard> request)
        {
            var query = new GetFilteredFlashCardsQuery<FlashCard>(request.Predicate, request.Page, request.Limit);

            var flashCardResults = await _mediator.Send(query);

            var response = flashCardResults.Select(fcr
                => new FlashCardResponse(
                    fcr.Id,
                    fcr.UserId,
                    fcr.User?.Username,
                    fcr.Word,
                    fcr.WordTranslation,
                    fcr.Description,
                    fcr.Tags.Select(t => t.Id).ToList(),
                    fcr.IsShared))
                .ToList();

            return response;
        }
    }
}