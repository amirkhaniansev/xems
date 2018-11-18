using System.Collections.Generic;
using System.Threading.Tasks;
using ExamsAPI.Filters;
using ExamsAPI.Models;

namespace ExamsAPI.Repositories.Interfaces
{
    public interface IExamRepository
    {
        Task Create(Exam exam);

        Task<Exam> GetExamById(string id);

        Task<IEnumerable<Exam>> GetAllExams(ExamFilter filter);
    }
}