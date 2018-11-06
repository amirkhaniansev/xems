using System;

namespace AuthAPI.Models
{
    /// <summary>
    /// Class for session
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets IP address
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets start time
        /// </summary>
        public DateTimeOffset StartTime { get; set; }

        /// <summary>
        /// Gets or sets end time
        /// </summary>
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// Gets or sets session description
        /// </summary>
        public string Description { get; set; }
    }
}