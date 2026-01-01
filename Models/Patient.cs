using System;

namespace HospitalSystem.Models
{
    public class Patient
    {
        public int Id {get; set;}
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth {get; set; }

        public Patient(int id, string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
        }
    }
}