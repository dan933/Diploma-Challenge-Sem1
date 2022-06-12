using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.models
{
    public class OwnerRequest
    {
        public OwnerRequest(){

        }
        
        public string? Surname { get; set; }

        public string? Firstname { get; set; }

        public string? Phone { get; set; }
    }
}