using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure.DbInitializers;

namespace Corelia.DataLake.Dashboard.Apis.Extentions
{
    public static class InitializerExtension
    {
        public async static Task<WebApplication> InitializerEventManagmentContextAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var CoreliaContextIntializer = services.GetRequiredService<ICoreliaDataLakeDbInitializer>();
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await CoreliaContextIntializer.InitializeAsync();
                await CoreliaContextIntializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "an error has been occured during applaying migrations");
            }

            return app;
        }
    }
}
