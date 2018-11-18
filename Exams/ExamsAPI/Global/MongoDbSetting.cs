/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API MongoDb Settings
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

namespace ExamsAPI.Global
{
    /// <summary>
    /// Class for MongoDB settings
    /// </summary>
    public class MongoDbSetting
    {
        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or set database name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}