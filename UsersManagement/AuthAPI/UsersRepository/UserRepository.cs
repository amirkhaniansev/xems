using System.Threading.Tasks;
using AccessCore.Repository;
using AuthAPI.Globals;
using AuthAPI.Models;

namespace AuthAPI.UsersRepository
{
    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository:IUserRepository
    {
        /// <summary>
        /// Repository
        /// </summary>
        private DataManager _dm;

        /// <summary>
        /// Creates new instance of User repository
        /// </summary>
        /// <param name="dm">Data manager</param>
        public UserRepository(DataManager dm)
        {
            this._dm = dm;
        }

        /// <summary>
        /// Finds user by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>user</returns>
        public Task<User> FindAsync(string username) =>
            Task.Run(() => (User)this._dm.Operate<string, User>(Constants.GetUserByUsername, username));

        /// <summary>
        /// Finds user by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>user</returns>
        public Task<User> FindAsync(int id) =>
            Task.Run(() => (User) this._dm.Operate<int, User>(Constants.GetUserById, id));
    }
}