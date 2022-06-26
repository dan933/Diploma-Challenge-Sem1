using System;
using System.Collections.Generic;

namespace API.Test
{
    public partial class Treatment
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string? PetName { get; set; }
        public int? ProcedureId { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public decimal? Payment { get; set; }
    }
}
