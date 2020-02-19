using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
     public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
