using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.NewModels
{
    public partial class Owner
    {
        public Owner()
        {
            Pets = new HashSet<Pet>();
        }
        
        public int OwnerId { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
