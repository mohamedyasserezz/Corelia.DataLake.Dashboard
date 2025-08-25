using Corelia.DataLake.Dashboard.Domain.Contract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Corelia.DataLake.Dashboard.Apis.Services
{
	public class LoggedInUserService : ILoggedInUserService
	{
		private readonly IHttpContextAccessor? _httpcontextAccessor;
		public string? UserId { get; set; }

		public LoggedInUserService(IHttpContextAccessor? contextAccessor)
		{
			_httpcontextAccessor = contextAccessor;


			UserId = _httpcontextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
				  ?? _httpcontextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid)
				  ?? _httpcontextAccessor?.HttpContext?.User.FindFirstValue(JwtRegisteredClaimNames.Sub);
		}
	}
}
