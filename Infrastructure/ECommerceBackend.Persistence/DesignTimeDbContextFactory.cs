using ECommerceBackend.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Persistence;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceBackendDbContext>
{
    public ECommerceBackendDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ECommerceBackendDbContext> optionsBuilder = new();
        optionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new ECommerceBackendDbContext(optionsBuilder.Options);
    }
}