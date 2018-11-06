using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace AuthAPI
{
    /// <summary>
    /// Class for configuration data.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets api resources.
        /// </summary>
        /// <returns>api resources</returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("offline_access"),
                new ApiResource("UsersAPI"),
                new ApiResource("ExamsAPI")
            };
        }

        /// <summary>
        /// Gets clients.
        /// </summary>
        /// <returns>clients</returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "DefaultClient",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "UsersAPI",
                        "ExamsAPI",
                    },
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    AbsoluteRefreshTokenLifetime = 15552000,
                    AccessTokenLifetime = 1800,
                    AllowOfflineAccess = true
                }
            };
        }

        /// <summary>
        /// Gets identity resources.
        /// </summary>
        /// <returns>identity resources</returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name = "Username",
                    UserClaims = new List<string> {"Username"}
                }
            };
        }
    }
}
