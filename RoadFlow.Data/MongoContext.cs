using MongoDB.Driver;
using RoadFlow.Common.Configurations;

namespace RoadFlow.Data;

public class MongoContext
{
    private readonly MongoClient _client;
    public IMongoDatabase Database { get; }

    public MongoContext(MongoDbConfiguration mongoDbConfiguration)
    {
        _client = new MongoClient(mongoDbConfiguration.ConnectionString);
        Database = _client.GetDatabase(mongoDbConfiguration.DatabaseName);
    }
}