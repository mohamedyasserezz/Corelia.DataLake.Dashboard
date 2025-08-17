using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Corelia.DataLake.Dashboard.Persistance.Data
{
    public class ApplicationDbContext(DbContextOptions options) :
       IdentityDbContext<ApplicationUser>(options)
    {
        // User-related DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
