using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedEntities.Entities
{
    public class VozUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
    }
}
