using Microsoft.AspNetCore.Identity;

namespace Corelia.DataLake.Dashboard.Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? Image { get; set; }
        public UserType UserType { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
