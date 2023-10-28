using FlashCards.Infrastructure.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FlashCards.Infrastructure.Common.Persistance.MongoDB
{
    public class AppSettingsMongoDb : IAppSettings
    {
        private IConfiguration _configuration;

        public AppSettingsMongoDb(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string MongoDbConnectionString => _configuration["ConnectionStrings:MongoDbConnectionString"];
        public string MongoDBdatabase => _configuration["Databases:MongoDbDatabase"];
    }
}