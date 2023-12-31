﻿using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Domain.Entities.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using System.Linq.Expressions;

namespace FlashCards.Infrastructure.Common.Persistance
{
    public class OnlineGenericRepository<TCollection> : IOnlineGenericRepository<TCollection> where TCollection : class, IIdentity, new()
    {
        private readonly IOnlineDbContext _context;

        public OnlineGenericRepository(IOnlineDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TCollection>> GetAsync(int page, int limit)
        {
            return await _context.GetAsync<TCollection>(page, limit);
        }

        public async Task<TCollection> GetItemByKeyAsync<TFilterType>(string keyName, TFilterType filter)
        {
            return await _context.GetItemByKeyAsync<TCollection, TFilterType>(keyName, filter);
        }

        public async Task<bool> AddItemAsync(TCollection item)
        {
            return await _context.AddItemAsync(item);
        }

        public async Task<IEnumerable<TCollection>> GetFilteredAsync(Expression<Func<TCollection, bool>> predicate, int page, int limit)
        {
            return await _context.GetFilteredAsync(predicate, page, limit);
        }

        public async Task<bool> UpdateItemAsync(TCollection item)
        {
            return await _context.UpdateItemAsync(item);
        }

        public async Task<bool> DeleteItemByIdAsync(string id)
        {
            return await _context.DeleteItemByIdAsync<TCollection>(id);
        }
    }
}