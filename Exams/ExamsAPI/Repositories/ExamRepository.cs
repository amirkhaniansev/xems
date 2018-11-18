using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExamsAPI.Filters;
using MongoDB.Driver;
using ExamsAPI.Global;
using ExamsAPI.Models;
using ExamsAPI.Repositories.Interfaces;

namespace ExamsAPI.Repositories
{
    public class ExamRepository : RepositoryBase , IExamRepository
    {
        public IMongoCollection<Exam> Exams => 
            this._db.GetCollection<Exam>(Constants.Exams);

        public ExamRepository(MongoDbSetting setting) 
            : base(setting) { }

        public async Task Create(Exam exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            await this.Exams.InsertOneAsync(exam);
        }

        public async Task<Exam> GetExamById(string id)
        {
            return await this.Exams.Find(exam => exam.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Exam>> GetAllExams(ExamFilter filter)
        {
            return await this.Exams.Find(FilterHelper.ConstructFilterDefinition(filter)).ToListAsync();
        }
    }
}