using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Infrastructure.Common.Persistance.Interfaces
{
    public interface IDbContext
    {
        Task<IEnumerable<TCollection>> GetAllAsync<TCollection>() where TCollection : class, new();
        Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class, new();
        Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class, new();
    }
}
