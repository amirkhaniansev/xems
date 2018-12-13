/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Question model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Collections.Generic;
using ExamsApiConsumer.Enums;

namespace ExamsApiConsumer.Models
{
    /// <summary>
    /// Model for question
    /// </summary>
    public class Question
    {              
        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets question type
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Gets or sets variants
        /// </summary>
        public List<Variant> Variants { get; set; }

        /// <summary>
        /// Gets or sets point
        /// </summary>
        public decimal? Point { get; set; }
    }
}