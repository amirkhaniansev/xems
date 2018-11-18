using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessCore.Repository;
using ExamsAPI.Filters;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using XemsLogger;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace ExamsAPI.Controllers
{    
    [ApiController]
    [Route("api/exams")]
    [Authorize(Policy = "IsLecturer")]
    [Produces("application/json")]
    public class ExamsController : ExamsApiControllerBase
    {
        private readonly IExamRepository _repository;

        public ExamsController() 
        {
            this._repository = Globals.ExamRepository;
        }

        [HttpPost]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var exam = await this._repository.GetExamById(id);

                if (exam == null)
                {
                    return this.NotFound();
                }

                return new JsonResult(exam);
            }
            catch (Exception ex)
            {
                this._logger.Log(LogHelper.CreateLog(DateTime.Now, LogType.Fatal, null,ex));

                return this.BadRequest();
            }
        }

        [HttpGet]
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

    }
}