using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;
using XemsLogger;
using XemsMailer.Mailers;

namespace UsersAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    [Produces("application/json")]
    public class UsersController : UsersApiBaseController
    {
        private readonly MessageMailer _mailer;

        public UsersController()
        {
            this._mailer = Globals.Mailer;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var user = await this._dm.OperateAsync<string, User>(Constants.GetUserByUsername, username);

                if (user == null)
                    return this.NotFound();

                return new JsonResult(user);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null, ex));

                return this.BadRequest();
            }
        }

    }
}