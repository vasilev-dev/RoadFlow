using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace RoadFlow.Common.Configurations;

public static class SharedConfigurationBuilder
{
    public static SharedConfiguration BindAndValidate(IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind("JWT", jwtConfiguration);

        var googleAuthConfiguration = new GoogleAuthConfiguration();
        configuration.Bind("GoogleAuth", googleAuthConfiguration);

        var mongoDbConfiguration = new MongoDbConfiguration();
        configuration.Bind("MongoDB", mongoDbConfiguration);

        var sharedConfiguration = new SharedConfiguration(jwtConfiguration, googleAuthConfiguration, mongoDbConfiguration);
        
        var validator = new SharedConfigurationValidator();
        validator.ValidateAndThrow(sharedConfiguration);

        return sharedConfiguration;
    }
}