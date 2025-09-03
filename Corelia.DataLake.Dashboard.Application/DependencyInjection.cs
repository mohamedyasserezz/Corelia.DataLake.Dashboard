using Corelia.DataLake.Dashboard.Application.Mapping;
using Corelia.DataLake.Dashboard.Application.Services.Authentication;
using Corelia.DataLake.Dashboard.Application.Services.Email;
using Corelia.DataLake.Dashboard.Application.Services.Files;
using Corelia.DataLake.Dashboard.Application.Services.Tasks;
using Corelia.DataLake.Dashboard.Application.Services.Workspaces;
using Corelia.DataLake.Dashboard.Domain.Contract;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.File;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Task;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Workspaces;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddScoped<IServiceManager, ServiceManager>();
			services.AddScoped<IJwtProvider, JwtProvider>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IFileService, FileService>();
			services.AddScoped<IEmailSender, EmailService>();
			services.AddScoped<IWorkspaceService, WorkspaceService>();

			services.AddScoped(typeof(Func<IWorkspaceService>), (serviceprovider) =>
			{
				return () => serviceprovider.GetRequiredService<IWorkspaceService>();

			});


			#region Hangfire
			services.AddHangfire(Configuration => Configuration
			 .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			 .UseSimpleAssemblyNameTypeSerializer()
			 .UseRecommendedSerializerSettings()
			 .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

			// Add the processing server as IHostedService
			services.AddHangfireServer();
			#endregion

			#region mapping



			services.AddAutoMapper(typeof(MappingProfile));

            //var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();
            //mapper.ConfigurationProvider.AssertConfigurationIsValid();

            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<MappingProfile>();
            //}, typeof(MappingProfile).Assembly, typeof(CommentProfileResolver).Assembly);

            //// Validate AutoMapper configuration 

            #endregion


            services.AddHttpClient<ITaskServices, TaskServices>(client =>
            {
                client.BaseAddress = new Uri("https://app.humansignal.com");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Token", "<YOUR_ACCESS_TOKEN>");
            });
            return services;
		}
	}
}
