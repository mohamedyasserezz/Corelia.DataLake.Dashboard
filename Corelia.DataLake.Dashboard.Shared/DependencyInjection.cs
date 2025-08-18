using Corelia.DataLake.Dashboard.Shared._Common.Settings;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Corelia.DataLake.Dashboard.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            return services;
        }
    }
}
