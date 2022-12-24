using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.MediatR;
using Serilog;
using Serilog.Events;

namespace RoadFlow.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRoadFlowAuthentication(this IServiceCollection services,
        IdentityConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
                options.RequireHttpsMetadata = false;
            });

        return services;
    }

    public static IServiceCollection AddRoadFlowMediatR(this IServiceCollection serviceCollection,
        params Assembly[] assemblies)
    {
        serviceCollection.AddMediatR(assemblies);
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        serviceCollection.AddTransient(typeof(IRequestPostProcessor<,>), typeof(EventSenderPostProcessor<,>));

        return serviceCollection;
    }

    public static IServiceCollection AddRoadFlowLogger(this IServiceCollection serviceCollection, Assembly assembly)
    {
        var template = $"[{{Timestamp:HH:mm:ss}} {{Level:u3}} {assembly.Location}] {{Message:lj}}{{NewLine}}{{Exception}}";
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Debug(outputTemplate: template)
            .WriteTo.Console(outputTemplate: template)
            .WriteTo.File("../logs/RoadFlowLogs.txt", 
                restrictedToMinimumLevel: LogEventLevel.Error,
                outputTemplate: template)
            .CreateLogger();

        serviceCollection.AddSingleton(Log.Logger);

        return serviceCollection;
    }
}