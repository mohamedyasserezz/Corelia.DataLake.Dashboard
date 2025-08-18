using Corelia.DataLake.Dashboard.Application.Services.Authentication;
using Corelia.DataLake.Dashboard.Application.Services.Email;
using Corelia.DataLake.Dashboard.Application.Services.Files;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.Authentication;
using Corelia.DataLake.Dashboard.Domain.Contract.Service.File;
using Hangfire;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corelia.DataLake.Dashboard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

             services.AddScoped<IJwtProvider, JwtProvider>();
             services.AddScoped<IAuthService, AuthService>();
             services.AddScoped<IFileService, FileService>();
             services.AddScoped<IEmailSender, EmailService>();


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


  
            //services.AddAutoMapper(typeof(MappingProfile));

            ////var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();
            ////mapper.ConfigurationProvider.AssertConfigurationIsValid();

            //services.AddAutoMapper(config =>
            //{
            //    config.AddProfile<MappingProfile>();
            //}, typeof(MappingProfile).Assembly, typeof(CommentProfileResolver).Assembly);

            //// Validate AutoMapper configuration 

            #endregion



            return services;
        }
    }
}
