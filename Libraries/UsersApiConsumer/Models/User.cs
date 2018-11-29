using System;

namespace UsersApiConsumer.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Gender { get; set; }

        public string CurrentProfileType { get; set; }

        public string Email { get; set; }
    }
}