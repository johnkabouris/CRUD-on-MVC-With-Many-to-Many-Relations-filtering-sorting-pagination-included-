using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainerAssignment.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="This Field Cannot be Empty")]
        public string Subject { get; set; }

        public virtual ICollection<Trainer> Trainers { get; set; }
    }
}