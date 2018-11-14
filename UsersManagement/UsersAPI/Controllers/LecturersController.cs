using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;
using XemsLogger;

namespace UsersAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/lecturers")]
    [Produces("application/json")]
    public class LecturersController : UsersApiBaseController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var result = await this._dm.OperateAsync<string, Lecturer>(Constants.GetLecturerByUsername, username);

                if (result == null)
                {
                    return this.NotFound(Constants.LecturerNotFound);
                }

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.LecturerSearchError, ex));

                return this.BadRequest(Constants.LecturerSearchError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this._dm.OperateAsync<Lecturer>(Constants.GetLecturers);

                if (result == null)
                {
                    return this.NoContent();
                }

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.LecturerSearchError, ex));

                return this.BadRequest(Constants.LecturerSearchError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LecturerBase lecturer)
        {
            try
            {
                if (lecturer.UserId != this.GetAuthenticatedUserId())
                {
                    this._logger.Log(LogHelper.CreateLog(
                        DateTime.Now, LogType.Fail, Constants.ForbiddenLectureCreation, null));

                    return this.Forbid();
                }

                var result = (Error) await this._dm.OperateAsync<LecturerBase, object>(
                    Constants.CreateLecturerProfile, lecturer);

                if (result == Error.LecturerAlreadyExists)
                {
                    this._logger.Log(LogHelper.CreateLog(
                        DateTime.Now, LogType.Fail, Constants.LecturerAlreadyExists, null));

                    return this.BadRequest(Constants.LecturerAlreadyExists);
                }

                return this.Ok($"Lecturer ID : {(int)result}");
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.LecturerCreationUnknownError, ex));

                return this.BadRequest(Constants.LecturerCreationUnknownError);
            }
        }
    }
}