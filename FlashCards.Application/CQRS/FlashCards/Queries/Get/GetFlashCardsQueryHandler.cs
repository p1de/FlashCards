using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Domain.Entities.FlashCards;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Queries.Get
{
    public class GetFlashCardsQueryHandler : IRequestHandler<GetFlashCardsQuery, List<FlashCardResult>>
    {
        private readonly ILogger<GetFlashCardsQueryHandler> _logger;
        private readonly IOnlineGenericRepository<FlashCard> _onlineFlashCardRepository;

        public GetFlashCardsQueryHandler(
            ILogger<GetFlashCardsQueryHandler> logger,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository)
        {
            _logger = logger;
            _onlineFlashCardRepository = onlineFlashCardRepository;
        }

        public async Task<List<FlashCardResult>> Handle(GetFlashCardsQuery query, CancellationToken cancellationToken)
        {
            var flashCards = await _onlineFlashCardRepository.GetFilteredAsync(fc => fc.UserId == query.UserId, query.Page, query.Limit);

            var flashCardsResults = flashCards.Select(fc 
                => new FlashCardResult(fc)).ToList();

            return flashCardsResults;
        }
    }
}