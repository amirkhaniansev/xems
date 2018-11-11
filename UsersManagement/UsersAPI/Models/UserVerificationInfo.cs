using System;

namespace UsersAPI.Models
{
    public class UserVerificationInfo
    {
        public int UserId { get; set; }

        public string VerificationKey { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}