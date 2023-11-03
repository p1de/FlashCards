using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SQLite;

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

        public async Task<IEnumerable<TCollection>> GetAllAsync<TCollection>() where TCollection : class, new()
        {
            var table = await GetTableAsync<TCollection>();
            return await table.ToListAsync();
        }

        public async Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class, new()
        {
            return await Execute<TCollection, TCollection>(async () => await _database.GetAsync<TCollection>(obj => obj.GetType().GetProperties().FirstOrDefault(prop => prop.Name == keyName).GetValue(obj).Equals(filter)));
        }

        public async Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class, new()
        {
            return await Execute<TCollection, bool>(async () => await _database.InsertAsync(item) > 0);
        }

        private async Task CreateTableIfNotExistsAsync<TCollection>() where TCollection : class, new()
        {
            await _database.CreateTableAsync<TCollection>();
        }

        private async Task<TResult> Execute<TCollection, TResult>(Func<Task<TResult>> action) where TCollection : class, new()
        {
            await CreateTableIfNotExistsAsync<TCollection>();
            return await action();
        }

        private async Task<AsyncTableQuery<TCollection>> GetTableAsync<TCollection>() where TCollection : class, new()
        {
            await CreateTableIfNotExistsAsync<TCollection>();
            return _database.Table<TCollection>();
        }
    }
}
