using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace my_project_1.Models
{
    public class contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string Massage { get; set; }
    }
}