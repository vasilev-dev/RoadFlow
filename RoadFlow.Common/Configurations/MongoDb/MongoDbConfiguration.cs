namespace RoadFlow.Common.Configurations.MongoDb;

public class MongoDbConfiguration
{
    /// <summary>
    /// From RoadFlow.Common secrets
    /// </summary>
    public string ConnectionString { get; set; }
    
    public string DatabaseName { get; set; }
}