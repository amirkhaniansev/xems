using System;

namespace UsersAPI.Models
{
    public class UserSignUpInfo
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public char Gender { get; set; }

        public string Username { get; set; }

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