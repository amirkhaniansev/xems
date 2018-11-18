/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Filter
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using ExamsAPI.Global;

namespace ExamsAPI.Filters
{
    /// <summary>
    /// Exam filter
    /// </summary>
    public class ExamFilter : FilterBase
    {
        /// <summary>
        /// Gets or sets Exam type
        /// </summary>
        public ExamType? ExamType { get; set; }

        /// <summary>
        /// Gets or sets creator user id
        /// </summary>
        public int? CreatorUserId { get; set; }

        /// <summary>
        /// Gets or sets creator profile id
        /// </summary>
        public int? CreatorProfileId { get; set; }

        /// <summary>
        /// Gets or sets duration of exam
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets max grade of exam
        /// </summary>
        public decimal? MaxGrade { get; set; }

        /// <summary>
        /// Gets or sets the boolean value indicating if the exam has grade
        /// </summary>
        public bool? HasGrade { get; set; }

        /// <summary>
        /// Gets or sets date from
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        /// Gets or sets date to
        /// </summary>
        public DateTime? To { get; set; }
    }
}