using RoadFlow.Identity.Core.Domains.Token;
using RoadFlow.Identity.Core.Domains.User;
using RoadFlow.Identity.Core.Domains.User.Common.Password;
using RoadFlow.Identity.Data.Domains.User;

namespace RoadFlow.Identity.API.IoC;

public static class ServiceCollectionExtensions
{
    public static void RegisterAppServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITokenService, TokenService>();
        serviceCollection.AddScoped<IPasswordService, PasswordService>();
    }

    public static void RegisterAppRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
    }
}