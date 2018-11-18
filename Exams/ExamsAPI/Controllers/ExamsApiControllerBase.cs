/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API base controller
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using XemsLogger;
using XemsMailer.Mailers;
using ExamsAPI.Global;

namespace ExamsAPI.Controllers
{
    /// <summary>
    /// Base controller for Exams API
    /// </summary>
    [ApiController]
    public class ExamsApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        protected readonly IXemsLogger _logger;

        /// <summary>
        /// Mailer
        /// </summary>
        protected readonly MessageMailer _mailer;

        /// <summary>
        /// Creates new instance of <see cref="ExamsApiControllerBase"/>
        /// </summary>
        public ExamsApiControllerBase()
        {
            this._logger = Globals.Logger;
            this._mailer = Globals.Mailer;
        }

        /// <summary>
        /// Gets id of authenticated user.
        /// </summary>
        /// <returns>id</returns>
        protected int GetAuthenticatedUserId()
        {
            var identity = this.User.Identity as ClaimsIdentity;

            if (identity == null)
                return 0;

            return int.Parse(
                identity.Claims.First(claim => claim.Type == "user_id").Value);
        }

        /// <summary>
        /// Checks if user current profile is student.
        /// </summary>
        /// <returns>boolean value which indicates if the user current profile is student.</returns>
        protected bool IsStudent()
        {
            var identity = this.User.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            return identity.Claims.First(claim => claim.Type == "current_profile").Value == "student";
        }
    }
}