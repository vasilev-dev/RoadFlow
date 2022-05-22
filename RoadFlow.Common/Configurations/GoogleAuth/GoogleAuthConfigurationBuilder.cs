using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RoadFlow.Common.Configurations.GoogleAuth;

public static class GoogleAuthConfigurationBuilder
{
    public static void AddGoogleAuthConfiguration(this WebApplicationBuilder builder)
    {
        var googleAuthConfiguration = new GoogleAuthConfiguration();

        builder.Configuration.Bind(googleAuthConfiguration);

        var validator = new GoogleAuthConfigurationValidator();
        var validationResult = validator.Validate(googleAuthConfiguration);
        if (!validationResult.IsValid)
        {
            var errorMessage = $"GoogleAuth configuration is invalid: {validationResult}";
            // todo add log
            throw new Exception(errorMessage);
        }

        builder.Services.AddSingleton(googleAuthConfiguration);
    }
}