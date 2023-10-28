using FlashCards.Infrastructure.Common.Interfaces;
using FlashCards.Infrastructure.Common.Persistance.Interfaces;
using MongoDB.Driver;

namespace FlashCards.Infrastructure.Common.Persistance.MongoDB
{
    public class MongoDbContext : IDbContext
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IAppSettings _appSettings;

        public MongoDbContext(IAppSettings appSettings)
        {
            try
            {
                _appSettings = appSettings;
                Lazy<MongoClient> lazyClient = new Lazy<MongoClient>(new MongoClient(_appSettings.MongoDbConnectionString));
                _client = lazyClient.Value;
                _database = _client.GetDatabase(_appSettings.MongoDBdatabase);
            }
            catch (Exception e)
            {
                Console.WriteLine("There was a problem connecting to your " +
                    "Atlas cluster. Check that the URI includes a valid " +
                    "username and password, and that your IP address is " +
                    $"in the Access List. Message: {e.Message}");
                Console.WriteLine(e);
                Console.WriteLine();
                throw;
            }
        }

        public async Task<IEnumerable<TCollection>> GetAllAsync<TCollection>() where TCollection : class
        {
            IMongoCollection<TCollection> collection;
            collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

            return await collection.FindAsync(Builders<TCollection>.Filter.Empty).Result.ToListAsync();
        }

        public async Task<TCollection> GetItemByKeyAsync<TCollection, TFilterType>(string keyName, TFilterType filter) where TCollection : class where TFilterType : class
        {
            IMongoCollection<TCollection> collection;
            collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

            return await collection.FindAsync(Builders<TCollection>.Filter.Eq(keyName, filter)).Result.FirstOrDefaultAsync();
        }

        public async Task<bool> AddItemAsync<TCollection>(TCollection item) where TCollection : class
        {
            IMongoCollection<TCollection> collection;
            collection = _database.GetCollection<TCollection>(typeof(TCollection).Name + "s");

            try
            {
                await collection.InsertOneAsync(item).ConfigureAwait(false);
            }
            catch
            {
                return false;
                throw;
            }

            return true;
        }
    }
}