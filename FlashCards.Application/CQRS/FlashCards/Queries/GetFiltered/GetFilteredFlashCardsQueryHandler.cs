using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.FlashCards.Common;
using FlashCards.Core.Application.CQRS.FlashCards.Queries.Get;
using FlashCards.Domain.Entities.FlashCards;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.FlashCards.Queries.GetFiltered
{
    public class GetFilteredFlashCardsQueryHandler : IRequestHandler<GetFilteredFlashCardsQuery<FlashCard>, List<FlashCardResult>>
    {
        private readonly ILogger<GetFlashCardsQueryHandler> _logger;
        private readonly IOnlineGenericRepository<FlashCard> _onlineFlashCardRepository;

        public GetFilteredFlashCardsQueryHandler(
            ILogger<GetFlashCardsQueryHandler> logger,
            IOnlineGenericRepository<FlashCard> onlineFlashCardRepository)
        {
            _logger = logger;
            _onlineFlashCardRepository = onlineFlashCardRepository;
        }

        public async Task<List<FlashCardResult>> Handle(GetFilteredFlashCardsQuery<FlashCard> query, CancellationToken cancellationToken)
        {
            var flashCards = await _onlineFlashCardRepository.GetFilteredAsync(query.Predicate, query.Page, query.Limit);

            var flashCardsResults = flashCards.Select(fc
                => new FlashCardResult(fc)).ToList();

            return flashCardsResults;
        }
    }
}
