using FlashCards.Core.Application.CQRS.FlashCards.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Core.Application.CQRS.FlashCards.Queries.GetFiltered
{
    public record GetFilteredFlashCardsQuery<T>(
        Expression<Func<T, bool>> Predicate,
        int Page,
        int Limit
    ) : IRequest<List<FlashCardResult>>;
}
