using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace Ignite.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        public ApplicationDbContext(DbContextOptions options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("Server=latitude-acuit\\sqlexpress;Database=ignite-db;User Id=ignite-user;Password=2HrD5W81KqIeodF91PLI;MultipleActiveResultSets=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
