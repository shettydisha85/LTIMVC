using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstMVC.Models
{
    public class StudentInfo
    {
        [Key]
        public int StudentId { 
            get; set; 
        }
        [Required]
        public string StudentName
        {
            get;set;
        }
        public string Branch { get; set; }
        public int Sem { get; set; }
        public decimal AggrMarks { get; set; }
        public int? CourseId { get; set; }
        public CourseInfo courseInfo { get; set; } //CourseId is referred from CourseInfo
    }
}