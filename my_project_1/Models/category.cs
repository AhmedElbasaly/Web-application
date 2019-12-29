using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace my_project_1.Models
{
    public class category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="نوع الوظيفة")]
        public string categoryName { get; set; }
        [Required]
        
        [Display(Name ="وصف النوع")]
        public string categoryDescription { get; set; }
        

        public virtual ICollection<Job> Jobs { get; set; }
    }
}