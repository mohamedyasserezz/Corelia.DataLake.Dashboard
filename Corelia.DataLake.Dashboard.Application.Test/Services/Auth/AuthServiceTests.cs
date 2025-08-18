using Corelia.DataLake.Dashboard.Application.Services.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.File;
using Corelia.DataLake.Dashboard.Domain.Entities.Authentication;
using Corelia.DataLake.Dashboard.Shared._Common.Errors;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Claims;

namespace Corelia.DataLake.Dashboard.Application.Test.Services.Auth
{
    public class AuthServiceTests
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailSender _emailSender = A.Fake<IEmailSender>();

        private readonly IAuthService _authService;

        private ApplicationUser CreateTestUser(string email, string userId = null!)
        {
            return new ApplicationUser
            {
                Id = userId ?? Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                FullName = "Fake Name"
            };
        }
        public AuthServiceTests()
        {
            // Create fakes for all dependencies
            _userManager = A.Fake<UserManager<ApplicationUser>>(options =>
                options.WithArgumentsForConstructor(() =>
                    new UserManager<ApplicationUser>(A.Fake<IUserStore<ApplicationUser>>(), null!, null!, null!, null!, null!, null!, null!, null!)));

            _signInManager = A.Fake<SignInManager<ApplicationUser>>(options =>
                options.WithArgumentsForConstructor(() =>
                    new SignInManager<ApplicationUser>(_userManager, A.Fake<IHttpContextAccessor>(), A.Fake<IUserClaimsPrincipalFactory<ApplicationUser>>(), null!, null!, null!, null!)));

            _unitOfWork = A.Fake<IUnitOfWork>();
            _jwtProvider = A.Fake<IJwtProvider>();
            _fileService = A.Fake<IFileService>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            // Use real logger instead of fake - logging is infrastructure, not business logic
            _logger = NullLogger<AuthService>.Instance;

            // Create the service to be tested with real implementation and faked dependencies
            _authService = new AuthService(
                _userManager,
                _signInManager,
                _unitOfWork,
                _jwtProvider,
                _fileService,
                _httpContextAccessor,
                _emailSender,
                _logger);
        }

        [Fact]
        public async Task ChangePasswordAsync_InvalidJwtToken_ReturnsFailure()
        {
            // Arrange
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            var changePasswordDto = new ChangePasswordDto("oldPass", "newPass");

            // Act
            var result = await _authService.ChangePasswordAsync(claimsPrincipal, changePasswordDto, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserErrors.InvalidJwtToken, result.Error);
        }

        [Fact]
        public async Task ChangePasswordAsync_UserNotFound_ReturnsFailure()
        {
            // Arrange
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "123") };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var changePasswordDto = new ChangePasswordDto("oldPass", "newPass");

            A.CallTo(() => _userManager.FindByIdAsync("123"))!
                .Returns(Task.FromResult<ApplicationUser>(null));

            // Act
            var result = await _authService.ChangePasswordAsync(claimsPrincipal, changePasswordDto, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserErrors.UserNotFound, result.Error);
        }

        [Fact]
        public async Task ChangePasswordAsync_PasswordChangeFails_ReturnsFailure()
        {
            // Arrange
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "123") };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var changePasswordDto = new ChangePasswordDto("oldPass", "newPass");
            var user = new ApplicationUser();

            A.CallTo(() => _userManager.FindByIdAsync("123"))!
                .Returns(Task.FromResult(user));

            A.CallTo(() => _userManager.ChangePasswordAsync(user, "oldPass", "newPass"))
                .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError { Description = "Failed" })));

            // Act
            var result = await _authService.ChangePasswordAsync(claimsPrincipal, changePasswordDto, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(UserErrors.OperationFaild, result.Error);
        }
        [Fact]
        public async Task ChangePasswordAsync_SuccessfulChange_ReturnsSuccessWithToken()
        {
            // Arrange
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "123") };
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var changePasswordDto = new ChangePasswordDto("oldPass", "newPass");
            var user = new ApplicationUser();
            var roles = new List<string> { "User" };

            A.CallTo(() => _userManager.FindByIdAsync("123"))!
                .Returns(Task.FromResult(user));

            A.CallTo(() => _userManager.ChangePasswordAsync(user, "oldPass", "newPass"))
                .Returns(Task.FromResult(IdentityResult.Success));

            A.CallTo(() => _userManager.GetRolesAsync(user))
                .Returns(Task.FromResult<IList<string>>(roles));

            // Act
            var result = await _authService.ChangePasswordAsync(claimsPrincipal, changePasswordDto, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Password changed successfully.", result.Value.Message);
            Assert.NotNull(result.Value.Token);
        }

    }

}

