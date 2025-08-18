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

        public Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
