/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API JSON Helper
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using System.Text;

namespace ExamsAPI.Global
{
    /// <summary>
    /// Static class for doing JSON operations
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Converts HTTP query string to JSON
        /// </summary>
        /// <param name="queryString">query string</param>
        /// <returns>JSON string</returns>
        public static string ConvertQueryToJson(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
                return string.Empty;

            var sb = new StringBuilder();

            sb.Append("{\"");

            for (var counter = 1; counter < queryString.Length; counter++)
            {
                if (queryString[counter] == '=')
                    sb.Append("\":\"");
                else if (queryString[counter] == '&')
                    sb.Append("\",\"");
                else sb.Append(queryString[counter]);                
            }

            sb.Append("\"}");

            return sb.ToString();
        }
    }
}