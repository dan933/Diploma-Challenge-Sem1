using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class Pet
    {
        public Pet()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string? PetName { get; set; }
        public string? Type { get; set; }

        public virtual Owner? Owner { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
