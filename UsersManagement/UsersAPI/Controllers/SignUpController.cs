using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AccessCore.Repository;
using XemsLogger;
using XemsPasswordHash;
using XemsMailer.Mailers;
using UsersAPI.Models;
using XemsMailer.Exceptions;

namespace UsersAPI.Controllers
{    
    [ApiController]
    [Route("api/sign-up")]
    [Produces("application/json")]
    public class SignUpController : ControllerBase
    {
        private readonly DataManager _dm;

        private readonly IXemsLogger _logger;

        private readonly PasswordHashService _hasher;

        private readonly MessageMailer _mailer;

        public SignUpController()
        {
            this._dm = Globals.DataManager;
            this._logger = Globals.Logger;
            this._hasher = Globals.Hasher;
            this._mailer = Globals.Mailer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserSignUpInfo user)
        {
            try
            {
                user.Password = this._hasher.HashPassword(user.Password);

                var userId = (int) await this._dm.OperateAsync<UserSignUpInfo, object>(
                    Constants.CreateUser, user);

                if (userId == 0)
                {
                    this._logger.Log(LogHelper.CreateLog(
                        DateTime.Now, LogType.Fail, Constants.UserSignUpFail, null));

                    return this.BadRequest(Constants.UserSignUpFail);
                }

                await this._mailer.SendAsync(
                    user.Email, Constants.WelcomeSubject, false, Constants.WelcomeMessage);
               
                return this.Ok(Constants.UserCreationSuccess);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.UserCreationError, ex));

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}