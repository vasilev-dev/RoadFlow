using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RoadFlow.Common.Configurations.Jwt;

public static class JwtConfigurationBuilder
{
    public static void AddJwtConfiguration(this WebApplicationBuilder builder)
    {
        var jwtConfiguration = new JwtConfiguration();

        builder.Configuration.Bind(jwtConfiguration);

        var validator = new JwtConfigurationValidator();
        var validationResult = validator.Validate(jwtConfiguration);
        if (!validationResult.IsValid)
        {
            var errorMessage = $"Jwt configuration is invalid: {validationResult}";
            // todo add log
            throw new Exception(errorMessage);
        }

        builder.Services.AddSingleton(jwtConfiguration);
    }
}