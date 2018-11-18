/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Interface for Exams API Exam Repository
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using ExamsAPI.Filters;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Enums;

namespace ExamsAPI.Repositories.Interfaces
{
    /// <summary>
    /// Interface for Exam Repository
    /// </summary>
    public interface IExamRepository
    {
        /// <summary>
        /// Creates new exam
        /// </summary>
        /// <param name="exam">exam</param>
        /// <returns>void</returns>
        Task Create(Exam exam);

        /// <summary>
        /// Gets exam by id
        /// </summary>
        /// <param name="id">exam id</param>
        /// <returns>exam</returns>
        Task<Exam> GetExamById(string id);

        /// <summary>
        /// Gets exams by the given filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>enumerable collection of exams</returns>
        Task<IEnumerable<Exam>> GetAllExams(ExamFilter filter);

        /// <summary>
        /// Updates the exam
        /// </summary>
        /// <param name="examUpdateInfo">Exam update information</param>
        /// <param name="userId">User id</param>
        /// <returns>update result</returns>
        Task<UpdateResult> Update(ExamUpdateInfo examUpdateInfo, int userId);

        /// <summary>
        /// Deletes exam.
        /// </summary>
        /// <param name="id">Exam id</param>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        Task<DeletionResult> DeleteExam(string id, int userId);
    }
}