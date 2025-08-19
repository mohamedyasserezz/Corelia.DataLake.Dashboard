using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure.DbInitializers;
using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Corelia.DataLake.Dashboard.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Corelia.DataLake.Dashboard.Persistance.DbInitilizers
{
    public class CoreliaDataLakeDbInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : ICoreliaDataLakeDbInitializer
    {
        public async Task InitializeAsync()
        {

            var pendingmigration = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingmigration.Any())
            {
                await dbContext.Database.MigrateAsync();
            }
        }

        public async Task SeedAsync()
        {
            // Example: create default admin user
            if (!await dbContext.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    FullName = "Admin",
                    Email = "admin@example.com",
                    UserName = "admin@example.com",
                    UserType = UserType.Admin
                };
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

                dbContext.Users.Add(adminUser);
                await dbContext.SaveChangesAsync();
            }

            // Add other seed data here...
        }
    }
}
