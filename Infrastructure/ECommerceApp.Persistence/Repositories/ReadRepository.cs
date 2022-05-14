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

    public IQueryable<T> GetAll() => Table;

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate) => Table.Where(predicate);

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate) => (await Table.FirstOrDefaultAsync(predicate))!;

    public async Task<T> GetByIdAsync(string id) => (await Table.FindAsync(id))!;
}