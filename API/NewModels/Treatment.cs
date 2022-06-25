using System;
using System.Collections.Generic;

namespace API.NewModels
{
    public partial class Treatment
    {
        public int Id { get; set; }
        public int? Fk_PetId { get; set; }
        public int? Fk_ProcedureId { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public decimal? Payment { get; set; }

        public virtual Pet? Fk_Pet { get; set; }
        public virtual Procedure? Fk_Procedure { get; set; }
    }
    public partial class TreatmentReq
    {        
        public int? FkPetId { get; set; }
        public int? FkProcedureId { get; set; }
        public DateTime? Date { get; set; }
        public string? Notes { get; set; }
        public decimal? Payment { get; set; }
    }
}
