using Microsoft.Extensions.Configuration;

namespace FlashCards.Infrastructure.Common.Persistance.MongoDb
{
    public class AppSettingsMongoDb
    {
        private readonly IConfiguration _configuration;

        public AppSettingsMongoDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string MongoDbConnectionString => _configuration["ConnectionStrings:MongoDbConnectionString"];
        public string MongoDbDatabase => _configuration["Databases:MongoDbDatabase"];
    }
}