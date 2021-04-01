using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstMVC.Models
{
    public class CourseInfo
    {
        
        
            [Key]
            public int CourseId { get; set; }
            [Required]
            public string CourseName { get; set; }
        
            public ICollection<StudentInfo> Students { get; set; }
    }
}