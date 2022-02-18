using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RoadFlow.Common.Configurations;

namespace RoadFlow.Common.Auth;

public static class AuthenticationBuilderExtensions
{
    public static AuthenticationBuilder AddAuthenticationByJwtBearer(this AuthenticationBuilder authenticationBuilder,
        JwtConfiguration jwtConfiguration)
    {
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = jwtConfiguration.ValidateIssuerSigningKey,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.IssuerSigningKey)),
                ValidateIssuer = jwtConfiguration.ValidateIssuer,
                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidateAudience = jwtConfiguration.ValidateAudience,
                ValidAudience = jwtConfiguration.ValidAudience,
                RequireExpirationTime = jwtConfiguration.RequireExpirationTime,
                ValidateLifetime = jwtConfiguration.RequireExpirationTime
            };
        });

        return authenticationBuilder;
    }

    public static AuthenticationBuilder AddAuthenticationByGoogleOAuth(this AuthenticationBuilder authenticationBuilder,
        GoogleAuthConfiguration googleAuthConfiguration)
    {
        authenticationBuilder.AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = googleAuthConfiguration.ClientId;
            googleOptions.ClientSecret = googleAuthConfiguration.ClientSecret;
        });

        return authenticationBuilder;
    }
}