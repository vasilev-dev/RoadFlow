using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using RoadFlow.Common.Configurations.GoogleAuth;
using RoadFlow.Common.Configurations.Jwt;
using RoadFlow.Common.Configurations.MongoDb;

namespace RoadFlow.Common.Configurations;

public static class ConfigurationBuilderExtensions
{
    public static void AddRoadFlowApplicationConfigurations(this WebApplicationBuilder builder)
    {
        var environment = builder.Environment;
        var configurationBuilder = builder.Configuration;
        
        var commonConfigurationFolder = Path.Combine(environment.ContentRootPath, "..", "RoadFlow.Common", "Configurations");

        // todo check need setup this for appsettings.json / appsettings/{environment}.json
        configurationBuilder
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
            .AddJsonFile(Path.Combine(commonConfigurationFolder, "Jwt", "jwtConfiguration.json"), false)
            .AddJsonFile(Path.Combine(commonConfigurationFolder, "MongoDb", "mongoDbConfiguration.json"), false);
        
        configurationBuilder.AddEnvironmentVariables();
        
        builder.Configuration.AddUserSecrets<JwtConfiguration>();
        builder.Configuration.AddUserSecrets<MongoDbConfiguration>();
        
        builder.AddJwtConfiguration();
        builder.AddGoogleAuthConfiguration();
        builder.AddMongoDbConfiguration();
    }
}