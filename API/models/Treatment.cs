using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.models
{
    public class Treatment
    {
        public Treatment(){

        }

        public int OwnerId { get; set; }

        public string? PetName { get; set; }

        public int ProcedureID { get; set; }

        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public Decimal Payment { get; set; }
    }
}