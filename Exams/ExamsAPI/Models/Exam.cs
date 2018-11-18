/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ExamsAPI.Global;

namespace ExamsAPI.Models
{
    /// <summary>
    /// Class of Exam
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets exam type
        /// </summary>
        public ExamType ExamType { get; set; }

        /// <summary>
        /// Gets or sets subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets notes
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets tests
        /// </summary>
        public List<Test> Tests { get; set; }

        /// <summary>
        /// Gets or sets creator user id
        /// </summary>
        public int CreatorUserId { get; set; }

        /// <summary>
        /// Gets or sets creator profile id
        /// </summary>
        public int CreatorProfileId { get; set; }

        /// <summary>
        /// Gets or sets exam duration
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets max grade
        /// </summary>
        public decimal? MaxGrade { get; set; }

        /// <summary>
        /// Gets or sets boolean value indicating if exam has grade
        /// </summary>
        public bool HasGrade { get; set; }

        /// <summary>
        /// Gets or sets modifiers
        /// </summary>
        public List<int> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets students
        /// </summary>
        public List<int> Students { get; set; }
    }
}