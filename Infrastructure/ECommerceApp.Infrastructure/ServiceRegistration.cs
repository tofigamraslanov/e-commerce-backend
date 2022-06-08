using ECommerceApp.Application.Services;
using ECommerceApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceApp.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}