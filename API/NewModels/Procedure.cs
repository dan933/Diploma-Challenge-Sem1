using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class Procedure
    {
        public Procedure()
        {            
        }

        public int Id { get; set; }
        public int? ProcedureId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
