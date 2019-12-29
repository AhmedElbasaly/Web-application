using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_project_1.Models
{
    public class JobViewModel
    {
        public string JobTitle { get; set; }
        public IEnumerable<Applyforjob> Items { get; set; }
    }
}