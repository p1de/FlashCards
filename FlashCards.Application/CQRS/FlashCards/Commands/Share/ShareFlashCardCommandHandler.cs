using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Share
{
    public class ShareFlashCardCommandHandler : IRequestHandler<ShareFlashCardCommand, FlashCardResult>
    {
        private readonly ILogger<ShareFlashCardCommandHandler> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IOnlineGenericRepository<FlashCard> _onlineFlashCardRepository;
        private readonly IOnlineGenericRepository<User> _onlineUserRepository;

        public ShareFlashCardCommandHandler(
            ILogger<ShareFlashCardCommandHandler> logger,
            IJwtTokenGenerator jwtTokenGenerator,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository,
            IOnlineGenericRepository<User> onlineUserRepository)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _onlineFlashCardRepository = onlineFlashCardRepository;
            _onlineUserRepository = onlineUserRepository;
        }

        public async Task<FlashCardResult> Handle(ShareFlashCardCommand command, CancellationToken cancellationToken)
        {
            var user = await _onlineUserRepository.GetItemByKeyAsync("Id", command.UserId);
            var flashCard = new FlashCard
            {
                UserId = command.UserId,
                User = new UserBasicInfo(user),
                Word = command.Word,
                WordTranslation = command.WordTranslation,
                Description = command.Description,
                Tags = command.Tags
            };

            await _onlineFlashCardRepository.AddItemAsync(flashCard);

            return new FlashCardResult(flashCard.Id, flashCard.UserId ?? "", flashCard.User ?? new UserBasicInfo(), flashCard.Word, flashCard.WordTranslation, flashCard.Description, flashCard.Tags ?? new List<Tag>());
        }
    }
}
