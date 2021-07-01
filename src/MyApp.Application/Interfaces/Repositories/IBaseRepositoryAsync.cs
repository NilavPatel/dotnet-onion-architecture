using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Domain.Models;
using MyApp.Domain.Specifications;

namespace MyApp.Application.Interfaces.Repositories
{
    public interface IBaseRepositoryAsync<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync(ISpecification<T> spec);
    }
}
