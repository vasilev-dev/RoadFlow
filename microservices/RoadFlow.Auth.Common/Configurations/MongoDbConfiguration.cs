namespace RoadFlow.Auth.Common.Configurations;

public class MongoDbConfiguration
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string FullConnectionString => $"{ConnectionString}/{DatabaseName}";
}