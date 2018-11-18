/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Variant model
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

namespace ExamsAPI.Models
{
    /// <summary>
    /// Model for variant
    /// </summary>
    public class Variant
    {
        /// <summary>
        /// Gets or sets variant symbol
        /// </summary>
        public string VariantSymbol { get; set; }

        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text { get; set; }
    }
}