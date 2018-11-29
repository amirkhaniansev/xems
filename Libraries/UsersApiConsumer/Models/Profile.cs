using System;

namespace UsersApiConsumer.Models
{
    public class Profile : ProfileBase
    {
        public int ProfileId { get; set; }
        
        public DateTime? CreationDate { get; set; }
    }
}