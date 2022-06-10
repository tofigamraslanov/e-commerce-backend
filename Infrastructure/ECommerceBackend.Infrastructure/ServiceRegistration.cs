using ECommerceBackend.Application.Services;
using ECommerceBackend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}