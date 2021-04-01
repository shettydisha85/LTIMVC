using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CFAssign.Models
{
    public class Publisher
    {
        [Key]
        public int pid { get; set; }
        [Required]
        public string pname { get; set; }
        [Required]
        public long mob_no { get; set; }
    }
}