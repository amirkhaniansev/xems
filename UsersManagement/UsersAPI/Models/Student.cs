using System;

namespace UsersAPI.Models
{
    public class Student : Profile
    {
        public int Id { get; set; }

        public string Department { get; set; }

        public DateTime? EntranceDate { get; set; }
        
    }
}