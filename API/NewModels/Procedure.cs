using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class Procedure
    {
        public Procedure()
        {
            Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public int? ProcedureId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Treatment> Treatments { get; set; }
    }
}
