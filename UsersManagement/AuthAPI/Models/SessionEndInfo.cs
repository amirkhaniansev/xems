namespace AuthAPI.Models
{
    /// <summary>
    /// Class for session deletion information
    /// </summary>
    public class SessionEndInfo
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets session id
        /// </summary>
        public int SessionId { get; set; }
    }
}