﻿using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Common;
using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly ECommerceBackendDbContext _context;

    public WriteRepository(ECommerceBackendDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T entity)
    {
        var entityEntry = await Table.AddAsync(entity);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> entities)
    {
        await Table.AddRangeAsync(entities);
        return true;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var entityEntry = await Task.Run(() => Table.Update(entity));
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        var entityEntry = await Task.Run(() => Table.Remove(entity));
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveRangeAsync(List<T> entities)
    {
        await Task.Run(() => Table.RemoveRange(entities));
        return true;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var entity = await Table.FindAsync(Guid.Parse(id));
        return await RemoveAsync(entity!);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}

