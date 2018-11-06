using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace AuthAPI.Globals
{
    /// <summary>
    /// Helper class in Authentication API
    /// </summary>
    public static class Provider
    {
        /// <summary>
        /// Gets id of authenticated user.
        /// </summary>
        /// <param name="identity">identity</param>
        /// <returns>id</returns>
        public static int GetAuthenticatedUserId(IIdentity identity) =>
            int.Parse(((ClaimsIdentity) identity).Claims.First(claim => claim.Type == "user_id").Value);
    }
}