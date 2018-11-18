/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Question Result model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

namespace ExamsAPI.Models
{
    /// <summary>
    /// Model for question result
    /// </summary>
    public class QuestionResult
    {
        /// <summary>
        /// Gets or sets note
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets answer
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Gets or sets point
        /// </summary>
        public decimal? Point { get; set; }

        /// <summary>
        /// Gets or sets right variant
        /// </summary>
        public Variant RightVariant { get; set; }
    }
}