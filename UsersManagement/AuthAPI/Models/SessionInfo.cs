using System;

namespace AuthAPI.Models
{
    /// <summary>
    /// Class for session
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets start time
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Gets or sets session description
        /// </summary>
        public string Description { get; set; }
    }
}