using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword;
using System.Security.Claims;

namespace Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication
{
    public interface IAuthService
    {
        // Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

        Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
        Task<Result<AuthResponse>> ConfirmEmailAsync(ConfirmEmailRequest request);
        // Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request);
        // Task<Result> SendResetPasswordOtpAsync(string email);
        Task<Result<ChangePasswordToReturn>> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, ChangePasswordDto changePasswordDto, CancellationToken cancellationToken);
        // Task<Result> AssignOtpForPassword(ResetPasswordRequest reset);
    }
}
