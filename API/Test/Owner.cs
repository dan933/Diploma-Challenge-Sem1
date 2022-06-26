using System;
using System.Collections.Generic;

namespace API.Test
{
    public class Owner
    {
        public Owner()
        {

        }

        public int OwnerId { get; set; }
        public string? UserId { get; set; }
        public string? Surname { get; set; }
        public string? Firstname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }        
    }
}
