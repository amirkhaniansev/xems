using System;

namespace UsersAPI.Models
{
    public class UserVerificationInfo : Verification
    {
        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}