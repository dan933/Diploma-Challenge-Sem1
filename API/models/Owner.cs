using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.models
{
    public class Owner
    {
        public Owner(){

        }

        public Owner(
            int ownerId, string? surname,
            string? firstName, string? phone)
        {
            OwnerId = ownerId;
            Surname = surname;
            Firstname = firstName;
            Phone = phone;
        }

        public int OwnerId { get; set; }

        public string? Surname { get; set; }

        public string? Firstname { get; set; }

        public string? Phone { get; set; }
    }
}