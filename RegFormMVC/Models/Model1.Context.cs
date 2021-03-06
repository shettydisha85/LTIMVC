//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RegFormMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class LTIMVCEntities : DbContext
    {
        public LTIMVCEntities()
            : base("name=LTIMVCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectInfo> ProjectInfoes { get; set; }
    
        public virtual ObjectResult<sp_SelectProjectbyId_Result> sp_SelectProjectbyId(Nullable<int> pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_SelectProjectbyId_Result>("sp_SelectProjectbyId", pidParameter);
        }
    
        public virtual int sp_updateProject(Nullable<int> pid, string pname, string domain)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("pid", pid) :
                new ObjectParameter("pid", typeof(int));
    
            var pnameParameter = pname != null ?
                new ObjectParameter("pname", pname) :
                new ObjectParameter("pname", typeof(string));
    
            var domainParameter = domain != null ?
                new ObjectParameter("domain", domain) :
                new ObjectParameter("domain", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_updateProject", pidParameter, pnameParameter, domainParameter);
        }
    }
}
