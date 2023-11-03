using FlashCards.Domain.Entities.Interfaces;
using System.Linq.Expressions;

namespace FlashCards.Infrastructure.Common.Persistance.Interfaces
{
    public interface IDbContext
    {
        Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new();

        Task<IEnumerable<TCollection>> GetAsync<TCollection>(int page, int limit) where TCollection : class, IIdentity, new();

        Task<IEnumerable<TCollection>> GetFilteredAsync<TCollection>(Expression<Func<TCollection, bool>> predicate, int page, int limit) where TCollection : class, IIdentity, new();

        Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class, IIdentity, new();

        Task<bool> UpdateItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new();
    }
}