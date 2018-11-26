/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Users API Lecturers Controller
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XemsLogger;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    /// <summary>
    /// Lectures controller
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/lecturers")]
    [Produces("application/json")]
    public class LecturersController : UsersApiBaseController
    {
        /// <summary>
        /// Gets lecturer by username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>action result</returns>
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

        /// <summary>
        /// Gets all lecturers
        /// </summary>
        /// <returns>lecturers</returns>
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

        /// <summary>
        /// Posts new lecturer
        /// </summary>
        /// <param name="lecturer">Lecturer</param>
        /// <returns>action result</returns>
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