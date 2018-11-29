using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccessCore.Repository;
using XemsLogger;
using XemsMailer.Mailers;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class VerificationsController : UsersApiBaseController
    {
        private readonly MessageMailer _mailer;
        
        public VerificationsController()
        {
            this._mailer = Globals.Mailer;
        }

        [HttpPost]
        [Route("api/verifications/{username}")]
        public async Task<IActionResult> Post(string username)
        {
            if (username == null)
                return this.NotFound();

            try
            {
                var userVerificationInfo = new UserVerificationInfo
                {
                    Username = username,
                    CreationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddHours(2)
                };

                using (var rng = new RNGCryptoServiceProvider())
                {
                    var buffer = new byte[8];

                    rng.GetBytes(buffer);

                    userVerificationInfo.VerificationKey = Convert.ToBase64String(buffer);
                }

                var result = (Error)await this._dm.OperateAsync<UserVerificationInfo, object>(
                    Constants.AddVerificationKey, userVerificationInfo);

                if (result == Error.VerificationCodeCreationFail)
                {
                    this._logger.Log(
                        LogHelper.CreateLog(DateTime.Now, LogType.Fail, Constants.VerificationKeyCreationFail, null));

                    return this.BadRequest(Constants.VerificationKeyCreationFail);
                }

                if (result == Error.VerificationCodeCreationSuccess)
                {
                    var user = (User) await this._dm.OperateAsync<string, User>(
                        Constants.GetUserByUsername, userVerificationInfo.Username);

                    await this._mailer.SendAsync(
                        user.Email, Constants.VerificationSubject, false, userVerificationInfo.VerificationKey);

                    return this.Ok(Constants.VerificationCreationSuccess);
                }

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this._logger.Log(
                    LogHelper.CreateLog(DateTime.Now, LogType.Fatal, Constants.VerificationKeyCreationFail, ex));

                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("api/verifications")]
        public async Task<IActionResult> Put([FromBody]Verification verification)
        {
            if (verification.Username == null)
                return this.NotFound();

            try
            {
                var result = (Error) await this._dm.OperateAsync<Verification, object>(
                    Constants.VerifyUser, verification);
                
                switch (result)
                {
                    case Error.VerificationSuccess:
                        return this.Ok(Constants.UserVerificationSuccess);

                    case Error.UserNotFound:
                        this._logger.Log(Constants.UserNotFoundMessage);
                        return this.NotFound(Constants.UserNotFoundMessage);

                    case Error.VerificationFail:
                        this._logger.Log(Constants.InvalidKeyMessage);
                        return this.BadRequest(Constants.InvalidKeyMessage);

                    case Error.ExpiredKey:
                        this._logger.Log(Constants.KeyIsExpired);
                        return this.BadRequest(Constants.KeyIsExpired);

                    default:
                        return this.BadRequest();
                }
            }
            catch (Exception ex)
            {
                this._logger.Log(
                    LogHelper.CreateLog(DateTime.Now, LogType.Fatal, Constants.VerificationError, ex));

                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}