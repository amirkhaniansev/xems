using MongoDB.Driver;
using ExamsAPI.Global;

namespace ExamsAPI.Repositories
{
    public class RepositoryBase
    {        
        private readonly MongoDbSetting _setting;

        protected readonly IMongoDatabase _db;

        public RepositoryBase(MongoDbSetting setting)
        {            
            var client = new MongoClient(setting.ConnectionString);
            
            this._setting = setting;
            this._db = client.GetDatabase(setting.DatabaseName);
        }
    }
}