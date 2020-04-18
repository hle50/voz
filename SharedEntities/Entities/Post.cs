using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedEntities.Entities
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate  { get; set; }
        public int CreatedByUserId { get; set; }
        public virtual VozUser VozUser { get; set; }
    }
}
