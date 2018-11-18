/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Result
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamsAPI.Models
{
    /// <summary>
    /// Model for exam result
    /// </summary>
    public class ExamResult
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets exam id
        /// </summary>
        public string ExamId { get; set; }

        /// <summary>
        /// Gets or sets notes
        /// </summary>
        public string Note { get; set; }
        
        /// <summary>
        /// Gets or sets grade
        /// </summary>
        public decimal? Grade { get; set; }

        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets test results
        /// </summary>
        public List<TestResult> TestResults { get; set; }

        /// <summary>
        /// Gets or sets boolean value which indicates whether the exam is checked
        /// </summary>
        public bool IsChecked { get; set; }
    }
}