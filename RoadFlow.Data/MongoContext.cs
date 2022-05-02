using MongoDB.Driver;
using RoadFlow.Common.Configurations;

namespace RoadFlow.Data;

public class MongoContext : IMongoContext
{
    public IMongoDatabase Database { get; }

    public MongoContext(MongoDbConfiguration mongoDbConfiguration)
    {
        var client = new MongoClient(mongoDbConfiguration.ConnectionString);
        Database = client.GetDatabase(mongoDbConfiguration.DatabaseName);
    }
}