/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Answer
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
    /// Model for Exam answer
    /// </summary>
    public class ExamAnswer
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
        /// Gets or sets student id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets test answers
        /// </summary>
        public List<TestAnswer> TestAnswers { get; set; }
    }
}