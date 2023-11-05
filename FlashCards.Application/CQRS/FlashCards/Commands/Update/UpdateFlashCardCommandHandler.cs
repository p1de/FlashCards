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
        private readonly IOnlineGenericRepository<User> _onlineUserRepository;

        public UpdateFlashCardCommandHandler(
            ILogger<UpdateFlashCardCommandHandler> logger,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository,
            IOnlineGenericRepository<User> onlineUserRepository)
        {
            _logger = logger;
            _onlineFlashCardRepository = onlineFlashCardRepository;
            _onlineUserRepository = onlineUserRepository;
        }

        public async Task<FlashCardResult> Handle(UpdateFlashCardCommand command, CancellationToken cancellationToken)
        {
            var flashCard = await _onlineFlashCardRepository.GetItemByKeyAsync("Id", command.Id);
            
            flashCard.Word = command.Word;
            flashCard.WordTranslation = command.WordTranslation;
            flashCard.Description = command.Description;
            flashCard.Tags = command.Tags;
            flashCard.IsShared = command.IsShared;

            await _onlineFlashCardRepository.UpdateItemAsync(flashCard);

            return new FlashCardResult(flashCard);
        }
    }
}
