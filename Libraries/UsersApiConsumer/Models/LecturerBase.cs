using System;

namespace UsersAPI.Models
{
    public class LecturerBase : ProfileBase
    {
        public string Degree { get; set; }

        public string Thesis { get; set; }

        public DateTime? WorkStartingDate { get; set; }
    }
}