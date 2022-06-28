using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using File = ECommerceBackend.Domain.Entities.File;

namespace ECommerceBackend.Persistence.Contexts;

public class ECommerceBackendDbContext : DbContext
{
    public ECommerceBackendDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<File> Files { get; set; } = null!;
    public DbSet<ProductImageFile> ProductImageFiles { get; set; } = null!;
    public DbSet<InvoiceFile> InvoiceFiles { get; set; } = null!;

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