namespace AuthAPI.Models
{
    /// <summary>
    /// Class for users
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets current profile type
        /// </summary>
        public string CurrentProfileType { get; set; }

        /// <summary>
        /// Gets or sets Activity
        /// </summary>
        public bool IsActive { get; set; } 

        /// <summary>
        /// Gets or sets IsVerified
        /// </summary>
        public bool IsVerified { get; set; }
    }
}