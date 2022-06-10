using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Application.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(List<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> RemoveAsync(T entity);
    Task<bool> RemoveRangeAsync(List<T> entities);
    Task<bool> RemoveAsync(string id);
    Task<int> SaveChangesAsync();
}