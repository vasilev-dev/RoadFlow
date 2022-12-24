using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoadFlow.Common.Configurations;

namespace RoadFlow.Common.Extensions;

public static class WebApplicationExtensions
{
    public static SharedConfiguration AddSharedConfiguration(this WebApplicationBuilder builder)
    {
        var folderWithSharedConfiguration = Path.Combine(builder.Environment.ContentRootPath, "..", 
            "RoadFlow.Common", "Configurations");
        
        builder.Configuration
            .AddJsonFile(Path.Combine(folderWithSharedConfiguration, "sharedSettings.json"))
            .AddJsonFile(Path.Combine(folderWithSharedConfiguration, 
                $"sharedSettings.{builder.Environment.EnvironmentName}.json"));

        var sharedConfiguration = new SharedConfiguration();
        builder.Configuration.Bind(sharedConfiguration);

        builder.Services.AddSingleton(sharedConfiguration);
        builder.Services.AddSingleton(sharedConfiguration.IdentityConfiguration);
        builder.Services.AddSingleton(sharedConfiguration.RabbitMQConfiguration);

        return sharedConfiguration;
    }
}