using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Models;
using XemsLogger;

namespace UsersAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/students")]
    [Produces("application/json")]
    public class StudentsController : UsersApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this._dm.OperateAsync<Student>(Constants.GetStudents);

                if (result == null)
                {
                    return this.NoContent();
                }

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.StudentSearchError, ex));

                return this.BadRequest(Constants.StudentSearchError);
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var result = await this._dm.OperateAsync<string, Student>(
                    Constants.GetStudentByUsername, username);

                if (result == null)
                {
                    return this.NoContent();
                }

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.StudentSearchError, ex));

                return this.BadRequest(Constants.StudentSearchError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentBase student)
        {
            try
            {
                if (student.UserId != this.GetAuthenticatedUserId())
                {
                    this._logger.Log(LogHelper.CreateLog(
                        DateTime.Now, LogType.Fail, Constants.ForbiddenStudentCreation, null));

                    return this.Forbid();
                }

                var result = (Error)await this._dm.OperateAsync<StudentBase, object>(
                    Constants.CreateStudentProfile, student);

                if (result == Error.StudentAlreadyExists)
                {
                    this._logger.Log(LogHelper.CreateLog(
                        DateTime.Now, LogType.Fail, Constants.StudentAlreadyExists, null));

                    return this.BadRequest(Constants.StudentAlreadyExists);
                }

                return this.Ok($"Student ID : {(int)result}");
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(
                    DateTime.Now, LogType.Fatal, Constants.StudentCreationUnknownError, ex));

                return this.BadRequest(Constants.StudentCreationUnknownError);
            }
        }
    }
}