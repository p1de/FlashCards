using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Update
{
    public class UpdateFlashCardCommandHandler : IRequestHandler<UpdateFlashCardCommand, FlashCardResult>
    {
        private readonly ILogger<UpdateFlashCardCommandHandler> _logger;
        private readonly IOnlineGenericRepository<FlashCard> _onlineFlashCardRepository;

        public UpdateFlashCardCommandHandler(
            ILogger<UpdateFlashCardCommandHandler> logger,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository)
        {
            _logger = logger;
            _onlineFlashCardRepository = onlineFlashCardRepository;
        }

        public async Task<FlashCardResult> Handle(UpdateFlashCardCommand command, CancellationToken cancellationToken)
        {
            var flashCard = new FlashCard
            {
                Id = command.Id,
                UserId = command.UserId,
                Word = command.Word,
                WordTranslation = command.WordTranslation,
                Description = command.Description,
                Tags = command.Tags
            };

            await _onlineFlashCardRepository.UpdateItemAsync(flashCard);

            return new FlashCardResult(flashCard.Id, flashCard.UserId ?? "", flashCard.User ?? new UserBasicInfo(), flashCard.Word, flashCard.WordTranslation, flashCard.Description, flashCard.Tags);
        }
    }
}
