using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedEntities.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEditDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? LastEditByUserId { get; set; }
        public virtual VozUser VozUser { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}
