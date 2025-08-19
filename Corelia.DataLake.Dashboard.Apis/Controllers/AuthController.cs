using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Shared.Abstraction;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.ConfirmEmail;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Login;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.RefreshToken;
using Corelia.DataLake.Dashboard.Shared.Models.Authentication.Register;
using Microsoft.AspNetCore.Mvc;

namespace Corelia.DataLake.Dashboard.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        IAuthService authService,
        ILogger<AuthController> logger
        ) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Register user with email: {email}", request.Email);

            var response = await _authService.RegisterAsync(request, cancellationToken);

            return response.IsSuccess
                ? Ok(response.Value)
                : response.ToProblem();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Logging with email: {email} and password: {password}", loginRequest.Email, loginRequest.Password);

            var response = await _authService.GetTokenAsync(loginRequest.Email, loginRequest.Password, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            var response = await _authService.GetRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : response.ToProblem();

        }

        [HttpPost("Revoke-refresh-Token")]
        public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] RefreshTokenRequest refreshTokenRequest, CancellationToken cancellationToken)
        {
            var response = await _authService.RevokeRefreshTokenAsync(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken, cancellationToken);

            return response.IsSuccess ? Ok() : response.ToProblem();

        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePasswordDto, CancellationToken cancellationToken)
        {
            var response = await _authService.ChangePasswordAsync(User, changePasswordDto, cancellationToken);
            return response.IsSuccess ? Ok(response.Value) : response.ToProblem();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.ConfirmEmailAsync(request);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

    }
}
