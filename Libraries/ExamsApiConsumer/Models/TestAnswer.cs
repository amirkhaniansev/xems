/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Test answer model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Collections.Generic;

namespace ExamsApiConsumer.Models
{
    /// <summary>
    /// Model for test answer
    /// </summary>
    public class TestAnswer
    {
        /// <summary>
        /// Gets or sets note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets question answers
        /// </summary>
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}