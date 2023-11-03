using FlashCards.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace FlashCards.Core.Application.Common.Interfaces.Persistance
{
    public interface IGenericRepository<TCollection> where TCollection : class, IIdentity, new()
    {
        Task<bool> AddItemAsync(TCollection item);

        Task<IEnumerable<TCollection>> GetAsync(int page, int limit);

        Task<TCollection> GetItemByKeyAsync<TFilterType>(string keyName, TFilterType filter);

        Task<IEnumerable<TCollection>> GetFilteredAsync(Expression<Func<TCollection, bool>> predicate, int page, int limit);

        Task<bool> UpdateItemAsync(TCollection item);
    }
}