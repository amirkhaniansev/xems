using System;
using ExamsAPI.Models;
using MongoDB.Driver;

namespace ExamsAPI.Filters
{
    public static class FilterHelper
    {
        public static FilterDefinition<Exam> ConstructFilterDefinition(ExamFilter filter)
        {
            var fdBuilder = Builders<Exam>.Filter;
            var fdef = fdBuilder.Empty;

            if (filter.CreatorProfileId.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.CreatorProfileId, filter.CreatorProfileId));

            if (filter.CreatorUserId.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.CreatorUserId, filter.CreatorUserId));

            if (filter.Duration.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.Duration, filter.Duration));

            if (filter.ExamType.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.ExamType, filter.ExamType));

            if (filter.HasGrade.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.HasGrade, filter.HasGrade));

            if (filter.From.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Gte(exam => exam.Created, filter.From));

            if (filter.To.HasValue)
                fdBuilder.And(fdef, fdBuilder.Lte(exam => exam.Created, filter.To));

            return fdef;
        }
    }
}