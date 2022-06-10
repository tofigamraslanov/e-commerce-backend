using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceBackend.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceBackendDbContext _context;

    public ReadRepository(ECommerceBackendDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool isTracking = true)
    {
        var query = Table.Where(predicate);
        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return (await query.FirstOrDefaultAsync(predicate))!;
    }

    public async Task<T> GetByIdAsync(string id, bool isTracking = true)
    {
        var query = Table.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return (await query.FirstOrDefaultAsync(t => t.Id == Guid.Parse(id)))!; // Marker pattern
    }
}