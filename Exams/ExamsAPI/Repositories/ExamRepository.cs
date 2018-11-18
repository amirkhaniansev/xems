/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Repository
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Filters;
using ExamsAPI.Repositories.Interfaces;
using UpdateResult = ExamsAPI.Repositories.Enums.UpdateResult;
using DeletionResult = ExamsAPI.Repositories.Enums.DeletionResult;

namespace ExamsAPI.Repositories
{
    /// <summary>
    /// Class for Exma repository
    /// </summary>
    public class ExamRepository : RepositoryBase , IExamRepository
    {
        /// <summary>
        /// Exams
        /// </summary>
        private readonly IMongoCollection<Exam> _exams;

        /// <summary>
        /// Creates new instance of <see cref="ExamRepository"/>
        /// </summary>
        /// <param name="setting">setting</param>
        public ExamRepository(MongoDbSetting setting)
            : base(setting)
        {
            this._exams = this._db.GetCollection<Exam>("Exams");
        }

        /// <summary>
        /// Creates new exam
        /// </summary>
        /// <param name="exam">exam</param>
        /// <returns>void</returns>
        public async Task Create(Exam exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            await this._exams.InsertOneAsync(exam);
        }

        /// <summary>
        /// Gets exam by id
        /// </summary>
        /// <param name="id">exam id</param>
        /// <returns>exam</returns>
        public async Task<Exam> GetExamById(string id)
        {
            return await this._exams.Find(exam => exam.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets exams by the given filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>enumerable collection of exams</returns>
        public async Task<IEnumerable<Exam>> GetAllExams(ExamFilter filter)
        {
            return await this._exams.Find(FilterHelper.ConstructFilterDefinition(filter)).ToListAsync();
        }

        /// <summary>
        /// Updates the exam
        /// </summary>
        /// <param name="examUpdateInfo">Exam update information</param>
        /// <param name="userId">User id</param>
        /// <returns>update result</returns>
        public async Task<UpdateResult> Update(ExamUpdateInfo examUpdateInfo, int userId)
        {
            var exam = await this.GetExamById(examUpdateInfo.ExamId);

            if (exam == null)
                return UpdateResult.NotFound;

            if (exam.CreatorUserId != userId || !exam.Modifiers.Contains(userId))
                return UpdateResult.NoAccess;

            if (exam.Created.AddMinutes(Convert.ToDouble(exam.Duration)) < DateTime.Now)
                return UpdateResult.ExamEnded;

            var updateDefBuilder = Builders<Exam>.Update;
            var updateDef = default(UpdateDefinition<Exam>);

            if (examUpdateInfo.Duration.HasValue)
                updateDef = updateDefBuilder.Set(ex => ex.Duration, examUpdateInfo.Duration);

            if (examUpdateInfo.Note != null)
                updateDef = updateDef.Set(ex => ex.Note, examUpdateInfo.Note);

            if (examUpdateInfo.Modifiers != null)
                updateDef = updateDef.Set(ex => ex.Modifiers, examUpdateInfo.Modifiers);

            if (examUpdateInfo.Students != null)
                updateDef = updateDef.Set(ex => ex.Students, examUpdateInfo.Students);

            if (examUpdateInfo.Tests != null)
                updateDef = updateDef.Set(ex => ex.Tests, examUpdateInfo.Tests);

            var updateResult = await this._exams.UpdateOneAsync(ex => ex.Id == exam.Id, updateDef);

            if (updateResult.IsAcknowledged)
                return UpdateResult.Success;

            return UpdateResult.Fail;
        }

        /// <summary>
        /// Deletes exam.
        /// </summary>
        /// <param name="id">Exam id</param>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        public async Task<DeletionResult> DeleteExam(string id, int userId)
        {
            var exam = await this.GetExamById(id);

            if (exam == null)
                return DeletionResult.NotFound;

            if (exam.CreatorUserId != userId || !exam.Modifiers.Contains(userId))
                return DeletionResult.NoAccess;

            var deletion = await this._exams.DeleteOneAsync(ex => ex.Id == exam.Id);

            if (deletion.IsAcknowledged && deletion.DeletedCount >= 1)
                return DeletionResult.Success;
            
            return DeletionResult.Fail;
        }
    }
}