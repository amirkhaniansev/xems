using System.Linq;
using System.Security.Claims;
using AccessCore.Repository;
using Microsoft.AspNetCore.Mvc;
using XemsLogger;

namespace UsersAPI.Controllers
{
    public class UsersApiBaseController : ControllerBase
    {
        protected readonly DataManager _dm;

        protected readonly IXemsLogger _logger;

        public UsersApiBaseController()
        {
            this._dm = Globals.DataManager;
            this._logger = Globals.Logger;
        }

        protected int GetAuthenticatedUserId()
        {
            var identity = this.User.Identity as ClaimsIdentity;

            if (identity == null)
                return 0;

            return int.Parse(
                identity.Claims.First(claim => claim.Type == "user_id").Value);
        }
    }
}