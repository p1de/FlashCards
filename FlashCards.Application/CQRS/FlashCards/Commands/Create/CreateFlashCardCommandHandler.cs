using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Create
{
    public class CreateFlashCardCommandHandler : IRequestHandler<CreateFlashCardCommand, FlashCardResult>
    {
        private readonly ILogger<CreateFlashCardCommandHandler> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IOfflineGenericRepository<FlashCard> _offlineFlashCardRepository;

        public CreateFlashCardCommandHandler(
            ILogger<CreateFlashCardCommandHandler> logger,
            IJwtTokenGenerator jwtTokenGenerator,
            IOfflineGenericRepository<FlashCard> offlineFlashCardRepository)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _offlineFlashCardRepository = offlineFlashCardRepository;
        }

        public async Task<FlashCardResult> Handle(CreateFlashCardCommand command, CancellationToken cancellationToken)
        {
            var flashCard = new FlashCard
            {
                Word = command.Word,
                WordTranslation = command.WordTranslation,
                Description = command.Description,
                Tags = command.Tags
            };

            await _offlineFlashCardRepository.AddItemAsync(flashCard);

            return new FlashCardResult(flashCard.Id, flashCard.UserId ?? "", flashCard.User ?? new UserBasicInfo(), flashCard.Word, flashCard.WordTranslation, flashCard.Description, flashCard.Tags);
        }
    }
}
