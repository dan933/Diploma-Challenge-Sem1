using System.ComponentModel.DataAnnotations;


namespace API.models
{
    public class Owner
    {
        public Owner(){

        }

        public int OwnerId { get; set; }

        public string? Surname { get; set; }

        public string? Firstname { get; set; }

        public string? Phone { get; set; }
    }
}