using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Infrastructure.Enums;
using ECommerceBackend.Infrastructure.Services.Storage;
using ECommerceBackend.Infrastructure.Services.Storage.Azure;
using ECommerceBackend.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
    {
        services.AddScoped<IStorage, T>();
    }

    public static void AddStorage(this IServiceCollection services, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                services.AddScoped<IStorage, LocalStorage>();
                break;
            case StorageType.Azure:
                services.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.AWS:
                break;
            default:
                services.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}