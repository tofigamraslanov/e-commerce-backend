using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Identity;
using ECommerceBackend.Persistence.Contexts;
using ECommerceBackend.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceBackendDbContext>(options =>
            options.UseNpgsql(Configuration.ConnectionString));
        services.AddIdentity<AppUser, AppUserRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
        }).AddEntityFrameworkStores<ECommerceBackendDbContext>();

        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        services.AddScoped<IFileReadRepository, FileReadRepository>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
    }
}