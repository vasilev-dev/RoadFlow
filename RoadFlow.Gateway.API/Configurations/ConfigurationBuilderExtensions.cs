using RoadFlow.Common.Configurations.GoogleAuth;
using RoadFlow.Common.Configurations.Jwt;

namespace RoadFlow.Gateway.API.Configurations;

public static class ConfigurationBuilderExtensions
{
    public static void AddApplicationConfigurations(this WebApplicationBuilder builder)
    {
        var environment = builder.Environment;
        var configurationBuilder = builder.Configuration;
        
        var commonConfigurationFolder = Path.Combine(environment.ContentRootPath, "..", "RoadFlow.Common", "Configurations");

        configurationBuilder
            .AddJsonFile("appsettings.json", true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
            .AddJsonFile("ocelot.json", true)
            .AddJsonFile($"ocelot.{environment.EnvironmentName}.json", true)
            .AddJsonFile(Path.Combine(commonConfigurationFolder, "JWT", "jwtConfiguration.json"), false);

        configurationBuilder.AddEnvironmentVariables();
        
        builder.Configuration.AddUserSecrets<JwtConfiguration>();
        builder.Configuration.AddUserSecrets<GoogleAuthConfiguration>();

        builder.AddJwtConfiguration();
        builder.AddGoogleAuthConfiguration();
    }
}