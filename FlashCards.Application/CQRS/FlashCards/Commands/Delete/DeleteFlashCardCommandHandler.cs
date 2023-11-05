using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Domain.Entities.FlashCards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Commands.Create
{
    public class DeleteFlashCardCommandHandler : IRequestHandler<DeleteFlashCardCommand, bool>
    {
        private readonly ILogger<DeleteFlashCardCommandHandler> _logger;
        private readonly IOnlineGenericRepository<FlashCard> _onlineFlashCardRepository;

        public DeleteFlashCardCommandHandler(
            ILogger<DeleteFlashCardCommandHandler> logger,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository)
        {
            _logger = logger;
            _onlineFlashCardRepository = onlineFlashCardRepository;
        }

        public async Task<bool> Handle(DeleteFlashCardCommand command, CancellationToken cancellationToken)
        {
            return await _onlineFlashCardRepository.DeleteItemByIdAsync(command.Id);
        }
    }
}