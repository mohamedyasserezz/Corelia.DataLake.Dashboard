using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corelia.DataLake.Dashboard.Persistance.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");

            builder.Property(x => x.FullName)
                .HasMaxLength(100);

            builder.Property(u => u.Image)
                .HasMaxLength(500);


            builder.Property(x => x.UserType)
                .HasConversion(

                    T => T.ToString(),
                    t => (UserType)System.Enum.Parse(typeof(UserType), t)
                );

            builder.OwnsMany(U => U.RefreshTokens)
                .ToTable("RefreshTokens")
                .WithOwner()
                .HasForeignKey("UserId");
            
        }
    }
}
