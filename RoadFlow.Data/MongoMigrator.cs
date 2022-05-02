using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RoadFlow.Identity.Core.Domains.User;
using Serilog;

namespace RoadFlow.Data;

public static class MongoMigrator
{
    public static void RunMigrations(this WebApplication app)
    {
        try
        {
            var context = app.Services.GetRequiredService<IMongoContext>();

            RunUserCollectionMigrations(context.Database);
        }
        catch (Exception ex)
        {
            var logger = app.Services.GetRequiredService<ILogger>();
            logger.Fatal(ex, "DB migration is failed");
        }
    }
    
    private static void RunUserCollectionMigrations(IMongoDatabase database)
    {
        var collection = database.GetCollection<User>("Users");

        var emailIndex = Builders<User>.IndexKeys.Ascending(user => user.Email);

        collection.Indexes.CreateOne(new CreateIndexModel<User>(emailIndex, new CreateIndexOptions { Unique = true }));
    }
}