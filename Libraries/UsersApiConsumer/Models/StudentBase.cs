using System;

namespace UsersApiConsumer.Models
{
    public class StudentBase : ProfileBase
    {
        public string Department { get; set; }

        public DateTime? EntranceDate { get; set; }
    }
}