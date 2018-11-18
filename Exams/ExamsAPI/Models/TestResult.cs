/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Test result model model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Collections.Generic;

namespace ExamsAPI.Models
{
    /// <summary>
    /// Model for test result
    /// </summary>
    public class TestResult
    {
        /// <summary>
        /// Gets or sets note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets point
        /// </summary>
        public decimal? Point { get; set; }

        /// <summary>
        /// Gets or sets question results
        /// </summary>
        public List<QuestionResult> QuestionResults { get; set; }
    }
}