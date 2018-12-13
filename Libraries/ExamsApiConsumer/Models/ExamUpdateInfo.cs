/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Exam Update information
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Collections.Generic;

namespace ExamsApiConsumer.Models
{
    /// <summary>
    /// Model for Exam Update Information
    /// </summary>
    public class ExamUpdateInfo
    {
        /// <summary>
        /// Gets or sets exam id
        /// </summary>
        public string ExamId { get; set; }

        /// <summary>
        /// Gets or sets note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets duration
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets tests
        /// </summary>
        public List<Test> Tests { get; set; }

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