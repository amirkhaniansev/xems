/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Answers Controller
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Interfaces;

namespace ExamsAPI.Controllers
{
    /// <summary>
    /// Exam Answers Controller
    /// </summary>
    [ApiController]
    [Route("api/exam-answers")]
    public class ExamAnswersController : ExamsApiControllerBase
    {
        /// <summary>
        /// Exam answers repository
        /// </summary>
        private readonly IExamAnswerRepository _examAnswerRepository;

        /// <summary>
        /// Creates new instance of <see cref="ExamAnswersController"/>
        /// </summary>
        public ExamAnswersController()
        {
            this._examAnswerRepository = Globals.ExamAnswerRepository;
        }

        /// <summary>
        /// Posts new exam answer
        /// </summary>
        /// <param name="examAnswer">Exam answer</param>
        /// <returns>action result</returns>
        [HttpPost]
        [Authorize(Policy = "IsStudent")]
        public async Task<IActionResult> Post([FromBody]ExamAnswer examAnswer)
        {
            // TODO
            return null;
        }
    }
}