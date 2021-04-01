using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirstMVC.Models
{
    public class LTICFEntities:DbContext
    {
            public LTICFEntities() : base("MyConn")
            {

            }
            public DbSet<CourseInfo> courseInfos { get; set; }
            public DbSet<StudentInfo> studentInfos { get; set; }
    }
}