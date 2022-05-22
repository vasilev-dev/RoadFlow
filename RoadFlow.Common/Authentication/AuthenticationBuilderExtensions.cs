using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RoadFlow.Common.Configurations.GoogleAuth;
using RoadFlow.Common.Configurations.Jwt;

namespace RoadFlow.Common.Authentication;

public static class AuthenticationBuilderExtensions
{
    public static void AddApplicationAuthentication(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var jwtConfiguration = provider.GetService<JwtConfiguration>();
        var googleAuthConfiguration = provider.GetService<GoogleAuthConfiguration>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/google/sign-in"; // todo should redirect to identity api
            })
            .AddAuthenticationByJwtBearer(jwtConfiguration!)
            .AddAuthenticationByGoogleOAuth(googleAuthConfiguration!);
    }
    
    private static AuthenticationBuilder AddAuthenticationByJwtBearer(
        this AuthenticationBuilder authenticationBuilder,
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
                ValidateLifetime = jwtConfiguration.RequireExpirationTime,
                ClockSkew = TimeSpan.Zero
            };
        });

        return authenticationBuilder;
    }

    // ReSharper disable once UnusedMethodReturnValue.Local
    private static AuthenticationBuilder AddAuthenticationByGoogleOAuth(
        this AuthenticationBuilder authenticationBuilder,
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