using MongoDB.Driver;

namespace RoadFlow.Data;

public interface IMongoContext
{
    IMongoDatabase Database { get; } 
}