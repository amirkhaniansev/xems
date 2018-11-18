/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Answer Repository
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Threading.Tasks;
using MongoDB.Driver;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Interfaces;

namespace ExamsAPI.Repositories
{
    /// <summary>
    /// Class for Exam Answer Repository
    /// </summary>
    public class ExamAnswerRepository : RepositoryBase, IExamAnswerRepository
    {
        /// <summary>
        /// Exam answers
        /// </summary>
        private readonly IMongoCollection<ExamAnswer> _examAnswers;

        /// <summary>
        /// Creates new instance of <see cref="ExamAnswerRepository"/>
        /// </summary>
        /// <param name="setting">setting</param>
        public ExamAnswerRepository(MongoDbSetting setting) 
            : base(setting)
        {
            this._examAnswers = this._db.GetCollection<ExamAnswer>("ExamAnswers");
        }

        /// <summary>
        /// Creates new exam
        /// </summary>
        /// <param name="exam">exam</param>
        /// <returns>void</returns>
        public async Task Create(ExamAnswer exam)
        {

        }      
    }
}