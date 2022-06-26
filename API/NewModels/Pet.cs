using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public class Pet
    {
        public Pet()
        {            
        }

        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string? PetName { get; set; }
        public string? Type { get; set; }
    }
}
