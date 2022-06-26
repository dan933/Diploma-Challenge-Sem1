using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.NewModels
{
    public class Owner
    {
        public Owner()
        {
                        
        }
        
        public int OwnerId { get; set; }
        public string? Surname { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
    }
}
