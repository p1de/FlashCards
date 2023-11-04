using FlashCards.Domain.Entities.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Reflection;

namespace FlashCards.Infrastructure.Common.Persistance.MongoDb
{
    public class MongoDbContext : IOnlineDbContext
    {
        private readonly ILogger<MongoDbContext> _logger;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly AppSettingsMongoDb _appSettings;

        public MongoDbContext(ILogger<MongoDbContext> logger, IConfiguration configuration)
        {
            _appSettings = new AppSettingsMongoDb(configuration);
            _logger = logger;
            try
            {
                Lazy<MongoClient> lazyClient = new(new MongoClient(_appSettings.MongoDbConnectionString));
                _client = lazyClient.Value;
                _database = _client.GetDatabase(_appSettings.MongoDbDatabase);
            }
            catch (Exception e)
            {
                _logger.LogError("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                _logger.LogError(message: "Exception: ", exception: e);
                throw;
            }
        }

        public async Task<IEnumerable<TCollection>> GetAsync<TCollection>(int page, int limit) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                var query = collection.Find(Builders<TCollection>.Filter.Empty).Skip(page * limit).Limit(limit).ToListAsync();

                await query;

                return query.Result;
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return new List<TCollection>();
            }
        }

        public async Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                return await collection.FindAsync(Builders<TCollection>.Filter.Eq(keyName, filter)).Result.FirstOrDefaultAsync();
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return new TCollection();
            }
        }

        public async Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                try
                {
                    await collection.InsertOneAsync(item).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return false;
                    throw;
                }

                return true;
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return false;
            }
        }

        public async Task<IEnumerable<TCollection>> GetFilteredAsync<TCollection>(Expression<Func<TCollection, bool>> predicate, int page, int limit) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                var query = collection.Find(predicate).Skip(page * limit).Limit(limit).ToListAsync();

                await query;

                return query.Result;
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return new List<TCollection>();
            }
        }

        public async Task<bool> UpdateItemAsync<TCollection>(TCollection item) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                try
                {
                    await collection.ReplaceOneAsync(x => x.Id == item.Id, item, new ReplaceOptions() { IsUpsert = true }).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return false;
                    throw;
                }

                return true;
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return false;
            }
        }

        public async Task<bool> DeleteItemByIdAsync<TCollection>(string id) where TCollection : class, IIdentity, new()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            if (accessType == NetworkAccess.Internet)
            {
                IMongoCollection<TCollection> collection;
                collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

                try
                {
                    await collection.DeleteOneAsync(x => x.Id == id).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    return false;
                    throw;
                }

                return true;
            }
            else
            {
                _logger.LogWarning("No internet connection, changes were made locally only.");
                return false;
            }
        }
    }
}