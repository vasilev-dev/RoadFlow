using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace RoadFlow.Common.Configurations;

public static class WebApplicationBuilderExtensionsForCustomConfiguration
{
    public static void SetupApplicationConfigurations(this WebApplicationBuilder builder)
    {
        var environment = builder.Environment;
        var configurationBuilder = builder.Configuration;
        var sharedFolder = Path.Combine(environment.ContentRootPath, "..", "RoadFlow.Common", "Configurations");
        
        configurationBuilder
            .AddJsonFile(Path.Combine(sharedFolder, "sharedConfiguration.json"), true)                    
            .AddJsonFile(Path.Combine(sharedFolder, $"sharedConfiguration.{environment.EnvironmentName}.json"), true)
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

        configurationBuilder.AddEnvironmentVariables();
        configurationBuilder.AddUserSecrets<JwtConfiguration>();
        configurationBuilder.AddUserSecrets<GoogleAuthConfiguration>();
        configurationBuilder.AddUserSecrets<MongoDbConfiguration>();
    }
}