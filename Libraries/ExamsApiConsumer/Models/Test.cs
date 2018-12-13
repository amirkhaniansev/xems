/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Test model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;
using System.Collections.Generic;

namespace ExamsApiConsumer.Models
{
    /// <summary>
    /// Model for test
    /// </summary>
    public class Test
    {
        /// <summary>
        /// Gets or sets creator id
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Gets or sets duration
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets point
        /// </summary>
        public decimal? Point { get; set; }

        /// <summary>
        /// Gets or sets created date
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets question
        /// </summary>
        public List<Question> Questions { get; set; }
    }
}