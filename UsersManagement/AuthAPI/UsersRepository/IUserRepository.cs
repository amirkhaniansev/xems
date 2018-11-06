using System.Threading.Tasks;
using AuthAPI.Models;

namespace AuthAPI.UsersRepository
{
    /// <summary>
    /// Interface for users repository
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Finds user by username.
        /// </summary>
        /// <param name="userName">Username.</param>
        /// <returns>user.</returns>
        Task<User> FindAsync(string userName);

        /// <summary>
        /// Finds user by id.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>user</returns>
        Task<User> FindAsync(int id);
    }
}