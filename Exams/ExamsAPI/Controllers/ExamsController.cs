/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exams Controller
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XemsLogger;
using ExamsAPI.Filters;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Enums;
using ExamsAPI.Repositories.Interfaces;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ExamsAPI.Controllers
{
    /// <summary>
    /// Exams controller
    /// </summary>
    [ApiController]
    [Route("api/exams")]
    [Produces("application/json")]
    public class ExamsController : ExamsApiControllerBase
    {
        /// <summary>
        /// Exams repository
        /// </summary>
        private readonly IExamRepository _repository;

        /// <summary>
        /// Creates new instance of <see cref="ExamsController"/>
        /// </summary>
        public ExamsController() 
        {
            this._repository = Globals.ExamRepository;
        }

        /// <summary>
        /// Gets exam by id.
        /// </summary>
        /// <param name="id">Exam Id</param>
        /// <returns>action result</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var exam = await this._repository.GetExamById(id);

                if (exam == null)
                {
                    return this.NotFound();
                }

                if (exam.CreatorUserId != this.GetAuthenticatedUserId())
                    return this.Forbid();

                return new JsonResult(exam);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null,ex));

                return this.BadRequest();
            }
        }

        /// <summary>
        /// Gets all exams.
        /// </summary>
        /// <returns>action result</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var queryString = this.Request.QueryString.ToString();
                var json = JsonHelper.ConvertQueryToJson(queryString);

                if (json == null)
                {
                    this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fail, "Invalid query", null));

                    return this.BadRequest();
                }

                var filter = JsonConvert.DeserializeObject<ExamFilter>(json);

                filter.CreatorUserId = this.GetAuthenticatedUserId();
                
                var exams = await this._repository.GetAllExams(filter);
                
                if (exams == null || !exams.Any())
                {
                    return this.NoContent();
                }

                return new JsonResult(exams);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null, ex));

                return this.BadRequest();
            }
        }

        /// <summary>
        /// Posts new exam.Requires "IsLecturer" policy.
        /// </summary>
        /// <param name="exam">Exam</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize(Policy = "IsLecturer")]
        public async Task<IActionResult> Post([FromBody]Exam exam)
        {
            try
            {
                await this._repository.Create(exam);

                return this.Ok();
            }
            catch (ArgumentNullException ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null, ex));

                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null, ex));

                return this.BadRequest();
            }
        }

        /// <summary>
        /// Updates exam. Requires "IsLecturer" policy.
        /// </summary>
        /// <param name="examUpdateInfo">Exam update information</param>
        /// <returns>action result</returns>
        [HttpPut]
        [Authorize(Policy = "IsLecturer")]
        public async Task<IActionResult> Put([FromBody]ExamUpdateInfo examUpdateInfo)
        {
            var actionResult = default(IActionResult);

            try
            {
                var updateResult = await this._repository.Update(examUpdateInfo,
                    this.GetAuthenticatedUserId());

                switch (updateResult)
                {
                    case UpdateResult.NotFound:
                        actionResult = this.NotFound();
                        break;
                    case UpdateResult.NoAccess:
                        actionResult = this.Forbid();
                        break;
                    case UpdateResult.Success:
                        actionResult = this.Ok();
                        break;
                    case UpdateResult.Fail:
                    case UpdateResult.ExamEnded:
                        actionResult = this.BadRequest();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null, ex));

                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return actionResult;
        }

        /// <summary>
        /// Deletes exam by id. Requires "IsLecturer" policy.
        /// </summary>
        /// <param name="id">Exam id</param>
        /// <returns>action result</returns>
        [HttpDelete]
        [Authorize(Policy = "IsLecturer")]
        public async Task<IActionResult> Delete(string id)
        {
            var actionResult = default(IActionResult);

            try
            {
                var deletionResult = await this._repository.DeleteExam(id, this.GetAuthenticatedUserId());
                
                switch (deletionResult)
                {
                    case DeletionResult.Success:
                        actionResult = this.Ok();
                        break;
                    case DeletionResult.NoAccess:
                        actionResult = this.Forbid();
                        break;
                    case DeletionResult.Fail:
                        actionResult = this.BadRequest();
                        break;
                    case DeletionResult.NotFound:
                        actionResult = this.NotFound();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog( DateTime.Now, LogType.Fatal, null, ex));

                actionResult = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return actionResult;
        }
    }
}