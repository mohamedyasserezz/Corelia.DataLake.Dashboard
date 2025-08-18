using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.File;
using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Corelia.DataLake.Dashboard.Shared._Common.Errors;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Corelia.DataLake.Dashboard.Application.Services.Authentication
{
    public class AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider,
        IFileService fileService,
        IHttpContextAccessor httpContextAccessor,
        IEmailSender emailSender,
        ILogger<AuthService> logger) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IFileService _fileService = fileService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly ILogger<AuthService> _logger = logger;


        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> _otpStore = new();
        private readonly int _otpExpiryMinutes = 15;
        private readonly int _refreshTokenExpiryDays = 14;


        public async Task<Result<AuthResponse>> GetTokenAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(email) is not { } user)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var result = await _signInManager.PasswordSignInAsync(user, password, false, true);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
                var refreshToken = GenerateRefreshToken();
                var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

                user.RefreshTokens.Add(new RefreshToken
                {
                    Token = refreshToken,
                    ExpiresOn = refreshTokenExpiration
                });

                await _userManager.UpdateAsync(user);

                var response = new AuthResponse(user.Id,
                    user.Email,
                    user.FullName,
                    _fileService.GetProfileUrl(user),
                    token,
                    expiresIn,
                    refreshToken,
                    user.UserType.ToString(),
                    refreshTokenExpiration);

                return Result.Success(response);
            }

            var error = result.IsNotAllowed
                ? UserErrors.EmailNotConfirmed
                : result.IsLockedOut
                ? UserErrors.LockedUser
                : UserErrors.InvalidCredentials;

            return Result.Failure<AuthResponse>(error);
        }

        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);



            if (user.LockoutEnd > DateTime.UtcNow)
                return Result.Failure<AuthResponse>(UserErrors.LockedUser);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var userRoles = await _userManager.GetRolesAsync(user);

            var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);
            var newRefreshToken = GenerateRefreshToken();
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresOn = refreshTokenExpiration
            });

            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id,
                user.Email,
                user.FullName,
                _fileService.GetProfileUrl(user),
                newToken,
                expiresIn,
                newRefreshToken,
                user.UserType.ToString(),
                refreshTokenExpiration);

            return Result.Success(response);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId = _jwtProvider.ValidateToken(token);

            if (userId is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return Result.Success();
        }

        public async Task<Result<ChangePasswordToReturn>> ChangePasswordAsync(ClaimsPrincipal claimsPrincipal, ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Result.Failure<ChangePasswordToReturn>(UserErrors.InvalidJwtToken);
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Result.Failure<ChangePasswordToReturn>(UserErrors.UserNotFound);
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            var userRoles = await _userManager.GetRolesAsync(user);

            var (token, expiresIn) = _jwtProvider.GenerateToken(user, userRoles);

            if (result.Succeeded)
            {
                // Optionally, you can send a confirmation email or log the password change
                _logger.LogInformation("User {UserId} changed their password successfully.", userId);
                return Result.Success(new ChangePasswordToReturn(
                              "Password changed successfully.",
                                  token
                                      ));
            }
            return Result.Failure<ChangePasswordToReturn>(UserErrors.OperationFaild);
        }



        #region Helpers
        private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        private static string GenerateOTP(int length = 4)
        {
            // Generate a numeric OTP of specified length
            Random random = new Random();
            return new string(Enumerable.Repeat(0, length)
                .Select(_ => random.Next(0, 10).ToString()[0])
                .ToArray());
        }


        #endregion
    }
}
