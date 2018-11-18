/* 
 * GNU General Public License Version 3.0, 29 June 2007
 * Class of Exams API Base Repository
 * Copyright (C) 2018  Sevak Amirkhanian
 * Email: amirkhanyan.sevak@gmail.com
 * For full notice please see https://github.com/amirkhaniansev/xems/blob/master/LICENSE.
 */

using MongoDB.Driver;
using ExamsAPI.Global;

namespace ExamsAPI.Repositories
{
    /// <summary>
    /// Class for repository base
    /// </summary>
    public class RepositoryBase
    {        
        /// <summary>
        /// MongoDB setting
        /// </summary>
        private readonly MongoDbSetting _setting;

        /// <summary>
        /// MongoDB database
        /// </summary>
        protected readonly IMongoDatabase _db;

        /// <summary>
        /// Creates new instance of <see cref="RepositoryBase"/>
        /// </summary>
        /// <param name="setting">setting</param>
        public RepositoryBase(MongoDbSetting setting)
        {            
            var client = new MongoClient(setting.ConnectionString);
            
            this._setting = setting;
            this._db = client.GetDatabase(setting.DatabaseName);
        }
    }
}