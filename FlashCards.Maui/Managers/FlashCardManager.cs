using FlashCards.Contracts.FlashCards;
using FlashCards.Core.Application.CQRS.FlashCards.Commands.Create;
using FlashCards.Core.Application.CQRS.FlashCards.Commands.Share;
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
            var command = new CreateFlashCardCommand(request.Word, request.WordTranslation, request.Description, new List<Tag>());

            var flashCardResult = await _mediator.Send(command);

            var response = new FlashCardResponse(
                flashCardResult.Id,
                flashCardResult.UserId,
                flashCardResult.Word,
                flashCardResult.WordTranslation,
                flashCardResult.Description,
                flashCardResult.Tags?.Select(t => t.Id).ToList());

            return response;
        }

        public async Task<FlashCardResponse> ShareFlashCard(ShareFlashCardRequest request)
        {
            var command = new ShareFlashCardCommand(request.UserId, request.Word, request.WordTranslation, request.Description, new List<Tag>());

            var flashCardResult = await _mediator.Send(command);

            var response = new FlashCardResponse(
                flashCardResult.Id,
                flashCardResult.UserId,
                flashCardResult.Word,
                flashCardResult.WordTranslation,
                flashCardResult.Description,
                flashCardResult.Tags?.Select(t => t.Id).ToList());

            return response;
        }
    }
}