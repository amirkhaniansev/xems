using System;
using UsersApiConsumer.Models;
using Xems.Globals;

namespace Xems.Models
{
    public class SignUpInfo : UserSignUpInfo
    {        
        public string ConfirmPassword { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public int? Day { get; set; }

        public DateTime Birthdate => new DateTime(
                    this.Year ?? 1900,
                    this.Month ?? 1,
                    this.Day ?? 1);
    }
}