using MyApp.Application.Core.Repositories;
using MyApp.Domain.Core.Models;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyAppDbContext _dbContext;
        private IDictionary<Type, dynamic> _repositories;

        public UnitOfWork(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, dynamic>();
        }

        public IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity
        {
            var entityType = typeof(T);
            if (_repositories.ContainsKey(entityType))
            {
                return _repositories[entityType];
            }

            var repositoryType = typeof(BaseRepositoryAsync<>);
            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            _repositories.Add(entityType, repository);
            return (IBaseRepositoryAsync<T>)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task RollBackChangesAsync()
        {
            await _dbContext.Database.RollbackTransactionAsync();
        }
    }
}