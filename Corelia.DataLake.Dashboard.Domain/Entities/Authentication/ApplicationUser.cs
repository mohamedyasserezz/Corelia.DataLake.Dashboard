using Microsoft.AspNetCore.Identity;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public int FullName { get; set; }
        public string? Image { get; set; }
        public UserType UserType { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
