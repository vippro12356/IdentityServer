//using IdentityServer4.EntityFramework.Entities;
//using IdentityServer4.Models;

//using IdentityServer4.EntityFramework.Entities;
//using IdentityServer4;
//using IdentityServer4.Models;

using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
        public static IEnumerable<ApiResource> GetApiResource()
        {
            return new List<ApiResource>()
            {
                new ApiResource("Test","Test API ")
            };
        }
        public static IEnumerable<ApiScope> GetApiScope()
        {
            return new List<ApiScope>()
            {
                new ApiScope("Test","Test API ")
            };
        }
        public static IEnumerable<Client> GetClient()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "Client",
                    ClientName = "Client Test ",
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris={"https://localhost:5107/" },
                    PostLogoutRedirectUris={"https://localhost:5107/" } ,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AccessTokenLifetime = 60*60*24,
                    IdentityTokenLifetime = 60*60*24,
                    RequireClientSecret = false,
                    AllowedGrantTypes=new List<string>
                    {
                        "password"
                    },
                    AllowedCorsOrigins = {"https://localhost:5107" } ,
                    RequireConsent=false,
                    RequirePkce=true,
                    AllowOfflineAccess=true,
                    AllowedScopes={

                        IdentityServerConstants.StandardScopes.OpenId,
                         IdentityServerConstants.StandardScopes.Profile,
                        "Test",
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };
        }
    }
}
