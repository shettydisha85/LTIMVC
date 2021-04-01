using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CFAssign.Models
{
    
        public class CFAssignEntities : DbContext
        {
            public CFAssignEntities() : base("MyConn")
            {

            }
            public DbSet<Publisher> Publishers { get; set; }
            public DbSet<Book> Books { get; set; }
        }
    
}