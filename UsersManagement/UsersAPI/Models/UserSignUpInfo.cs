using System;

namespace UsersAPI.Models
{
    public class UserSignUpInfo : User
    {       
        public DateTime BirthDate { get; set; }

        public string Phone { get; set; }
        
        public string Profession { get; set; }

        public string Description { get; set; }

        public string Password { get; set; }

        public string Continent { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string CityOrVillage { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string ZipCode { get; set; }
    }
}