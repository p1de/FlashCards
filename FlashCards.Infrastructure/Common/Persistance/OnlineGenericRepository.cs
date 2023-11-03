using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;

namespace FlashCards.Infrastructure.Common.Persistance
{
    public class OnlineGenericRepository<TCollection> : IOnlineGenericRepository<TCollection> where TCollection : class, new()
    {
        private readonly IOnlineDbContext _context;

        public OnlineGenericRepository(IOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TCollection>> GetAll()
        {
            return await _context.GetAllAsync<TCollection>();
        }

        public async Task<TCollection> GetItemByKeyAsync<TFilterType>(string keyName, TFilterType filter)
        {
            return await _context.GetItemByKeyAsync<TCollection, TFilterType>(keyName, filter);
        }

        public async Task<bool> AddItemAsync(TCollection item)
        {
            return await _context.AddItemAsync(item);
        }
    }
}