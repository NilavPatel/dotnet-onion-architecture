using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Data;

namespace MyApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MyAppDbContext _dbContext;
        private IDictionary<Type, dynamic> _repositories;
        private bool _disposed = false;

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

            //TODO: Need to find a way to use dependency injection with passing existing dbcontext object
            var repository = new BaseRepositoryAsync<T>(_dbContext);
            _repositories.Add(entityType, repository);
            return repository;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _dbContext.DisposeAsync();
                _disposed = true;
            }
        }
    }
}