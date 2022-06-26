using System;
using System.Collections.Generic;

namespace API.Test
{
    public class Pet
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string? PetName { get; set; }
        public string? Type { get; set; }

        public virtual Owner? Owner { get; set; }
    }
}
