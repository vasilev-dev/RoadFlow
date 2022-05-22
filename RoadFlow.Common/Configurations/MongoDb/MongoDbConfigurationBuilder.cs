using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RoadFlow.Common.Configurations.MongoDb;

public static class MongoDbConfigurationBuilder
{
    public static void AddMongoDbConfiguration(this WebApplicationBuilder builder)
    {
        var mongoDbConfiguration = new MongoDbConfiguration();

        builder.Configuration.Bind(mongoDbConfiguration);

        var validator = new MongoDbConfigurationValidator();
        var validationResult = validator.Validate(mongoDbConfiguration);
        if (!validationResult.IsValid)
        {
            var errorMessage = $"GoogleAuth configuration is invalid: {validationResult}";
            // todo add log
            throw new Exception(errorMessage);
        }

        builder.Services.AddSingleton(mongoDbConfiguration);
    }
}