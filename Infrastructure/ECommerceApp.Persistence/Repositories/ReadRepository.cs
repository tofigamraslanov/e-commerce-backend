using System.Linq.Expressions;
using ECommerceApp.Application.Repositories;
using ECommerceApp.Domain.Entities.Common;
using ECommerceApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ECommerceAppDbContext _context;

    public ReadRepository(ECommerceAppDbContext context)
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
        var query=Table.AsQueryable();
        if(!isTracking)
            query=query.AsNoTracking();
        
        return (await query.FirstOrDefaultAsync(t => t.Id == Guid.Parse(id)))!; // Marker pattern
    }
}