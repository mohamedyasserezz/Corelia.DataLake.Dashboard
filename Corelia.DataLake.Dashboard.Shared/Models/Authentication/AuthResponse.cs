using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication
{
    public record AuthResponse(
    string Id,
    string? Email,
    string FullName,
    string? Image,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    string UserType,
    DateTime RefreshTokenExpiration
);
}
