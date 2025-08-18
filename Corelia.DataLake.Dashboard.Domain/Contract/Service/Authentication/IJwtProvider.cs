using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication
{
    public interface IJwtProvider
    {
        (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles);
        string? ValidateToken(string token);
    }
}
