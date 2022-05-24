using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Persistence.Contexts;

public class ECommerceAppDbContext : DbContext
{
    public ECommerceAppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // ChangeTracker: It is the property that allows capturing the changes made or newly
        // added data over the entities. It allows us to capture and obtain the tracked data
        // in update operations.

        var entries = ChangeTracker.Entries<BaseEntity>();
        foreach (var entry in entries)
        {
            _ = entry.State switch
            {
                EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
                EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}