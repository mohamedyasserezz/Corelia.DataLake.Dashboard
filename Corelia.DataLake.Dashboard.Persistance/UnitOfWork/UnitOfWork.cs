using Corelia.DataLake.Dashboard.Domain.Contract.Infrastructure;
using Corelia.DataLake.Dashboard.Persistance.Data;
using Corelia.DataLake.Dashboard.Persistance.GenericRepository;
using System.Collections.Concurrent;

namespace Corelia.DataLake.Dashboard.Persistance.UnitOfWorks
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        #region Fields

        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();

        #endregion

        #region Implementation of IUnitOfWork
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();
        public ValueTask DisposeAsync() => _dbContext.DisposeAsync();

        public IGenricRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
            where TEntity : class
            where Tkey : IEquatable<Tkey>
        {
            return (IGenricRepository<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_dbContext));
        }
        #endregion
    }
}
