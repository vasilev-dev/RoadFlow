namespace RoadFlow.Common.Configurations;

public record SharedConfiguration(
    JwtConfiguration JwtConfiguration,
    GoogleAuthConfiguration GoogleAuthConfiguration,
    MongoDbConfiguration MongoDbConfiguration);