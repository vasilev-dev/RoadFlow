using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace RoadFlow.Auth.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] 
        { 
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new("RoadFlow.Showcase.API", "RoadFlow.Showcase.API"),
            new("RoadFlow.Auth.API", "RoadFlow.Auth.API")
        };

        public static IEnumerable<Client> Clients(string clientUrl)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "RoadFlow.PasswordClient",
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,

                    ClientUri = clientUrl,
                    RedirectUris = {$"{clientUrl}/showcase"},
                    PostLogoutRedirectUris = {$"{clientUrl}/sign-in"},
                    AllowedCorsOrigins = {clientUrl},

                    AllowedScopes =
                    {
                        "RoadFlow.Showcase.API",
                        "RoadFlow.Auth.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                }
            };
        }
    }
}