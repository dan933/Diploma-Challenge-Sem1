using System;
using System.Collections.Generic;

namespace API.Test
{
    public class Procedure
    {
        public Procedure()
        {
        }

        public int ProcedureId { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }        
    }
}
