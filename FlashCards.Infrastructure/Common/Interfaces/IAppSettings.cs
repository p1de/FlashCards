namespace FlashCards.Infrastructure.Common.Interfaces
{
    public interface IAppSettings
    {
        string MongoDbConnectionString { get; }
        string MongoDBdatabase { get; }
    }
}