namespace Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure.DbInitializers
{
    public interface ICoreliaDataLakeDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
