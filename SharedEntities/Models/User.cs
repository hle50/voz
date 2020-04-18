using System;
using System.Collections.Generic;
using System.Text;

namespace SharedEntities.Models
{
    
    public class User
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public string Token { get; set; }
        public IList<string> Role { get; set; }
    }
}
