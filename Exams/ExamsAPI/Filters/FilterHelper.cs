/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Filter Helper
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using ExamsAPI.Models;
using MongoDB.Driver;

namespace ExamsAPI.Filters
{
    /// <summary>
    /// Helper class for doing filtering operations
    /// </summary>
    public static class FilterHelper
    {
        /// <summary>
        /// Constructs filter definition from filter.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>filter definition</returns>
        public static FilterDefinition<Exam> ConstructFilterDefinition(ExamFilter filter)
        {
            var fdBuilder = Builders<Exam>.Filter;
            var fdef = fdBuilder.Empty;

            if (filter.CreatorProfileId.HasValue)
                fdef = fdBuilder.And(fdef, fdBuilder.Eq(exam => exam.CreatorProfileId, filter.CreatorProfileId));

            if (filter.CreatorUserId.HasValue)
            {
                var hasAccessDef = fdBuilder.Empty;

                hasAccessDef = fdBuilder.And(hasAccessDef, 
                    fdBuilder.Eq(exam => exam.CreatorUserId, filter.CreatorUserId));
                hasAccessDef = fdBuilder.Or(hasAccessDef, 
                    fdBuilder.AnyEq(exam => exam.Modifiers, filter.CreatorUserId.Value));
                hasAccessDef = fdBuilder.Or(hasAccessDef, 
                    fdBuilder.AnyEq(exam => exam.Students, filter.CreatorUserId.Value));

                fdef = fdBuilder.And(hasAccessDef, fdef);
            }

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