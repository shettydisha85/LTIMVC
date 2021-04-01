using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CFAssign.Models
{
    public class Book
    {
        [Key]
        public int ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author_name { get; set; }
        public string Category { get; set; }
        public decimal Money { get; set; }
        public int? Pid { get; set; }
        public Publisher publisher { get; set; }
    }
}