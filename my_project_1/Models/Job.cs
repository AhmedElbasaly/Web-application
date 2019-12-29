using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace my_project_1.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Display(Name ="اسم الوظيفة")]
        public string Jobtital { get; set; }
        [Display(Name = "وصف الوظيفة")]
        [AllowHtml]
        public string Jobcontent { get; set; }
        [Display(Name = "صورة الوظيفة")]
        public string intJobimage { get; set; }

        [Display(Name = "نوع الوظيفة")]
        public int CategoryId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual category category { get; set; }
       
    }
}