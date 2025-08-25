using Corelia.DataLake.Dashboard.Apis.Services;
using Corelia.DataLake.Dashboard.Domain.Contract;

namespace Corelia.DataLake.Dashboard.Apis.Extentions
{
	public static class DependencyInjection
	{
		public static IServiceCollection RegesteredPresestantLayer(this IServiceCollection services)
		{
			services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
			return services;
		}

	}
}
