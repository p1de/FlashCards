using FlashCards.Domain.Entities.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SQLite;
using System.Linq.Expressions;

namespace FlashCards.Infrastructure.Common.Persistance.SQLiteDb
{
    public class SQLiteDbContext : IOfflineDbContext
    {
        private readonly ILogger<SQLiteDbContext> _logger;
        private readonly AppSettingsSQLiteDb _appSettings;
        private readonly SQLiteAsyncConnection _database;

        public SQLiteDbContext(ILogger<SQLiteDbContext> logger, IConfiguration configuration)
        {
            _logger = logger;
            _appSettings = new AppSettingsSQLiteDb(configuration);
            _database = new SQLiteAsyncConnection(_appSettings.DatabasePath, _appSettings.Flags);
        }

        public async Task<IEnumerable<TCollection>> GetAsync<TCollection>(int page, int limit) where TCollection : class, IIdentity, new()
        {
            var table = await GetTableAsync<TCollection>();
            return await table.Skip(page * limit).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<TCollection>> GetFilteredAsync<TCollection>(Expression<Func<TCollection, bool>> predicate, int page, int limit) where TCollection : class, IIdentity, new()
        {
            var table = await GetTableAsync<TCollection>();
            return await table.Where(predicate).Skip(page * limit).Take(limit).ToListAsync();
        }

        public async Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class, IIdentity, new()
        {
            return await Execute<TCollection, TCollection>(async () => await _database.GetAsync<TCollection>(obj => obj.GetType().GetProperties().FirstOrDefault(prop => prop.Name == keyName).GetValue(obj).Equals(filter)));
        }

        public async Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new()
        {
            return await Execute<TCollection, bool>(async () => await _database.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new()
        {
            return await Execute<TCollection, bool>(async () => await _database.UpdateAsync(item) > 0);
        }

        private async Task CreateTableIfNotExistsAsync<TCollection>() where TCollection : class, IIdentity, new()
        {
            await _database.CreateTableAsync<TCollection>();
        }

        private async Task<TResult> Execute<TCollection, TResult>(Func<Task<TResult>> action) where TCollection : class, IIdentity, new()
        {
            await CreateTableIfNotExistsAsync<TCollection>();
            return await action();
        }

        private async Task<AsyncTableQuery<TCollection>> GetTableAsync<TCollection>() where TCollection : class, IIdentity, new()
        {
            await CreateTableIfNotExistsAsync<TCollection>();
            return _database.Table<TCollection>();
        }
    }
}
