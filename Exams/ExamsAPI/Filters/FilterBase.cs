/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Filter Base
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System;

namespace ExamsAPI.Filters
{
    /// <summary>
    /// Filter base class
    /// </summary>
    public class FilterBase
    {
        /// <summary>
        /// Gets or sets Created date
        /// </summary>
        public DateTime Created { get; set; }
    }
}