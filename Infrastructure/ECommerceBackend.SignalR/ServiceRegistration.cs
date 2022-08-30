using ECommerceBackend.Application.Abstractions.Hubs;
using ECommerceBackend.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.SignalR;

public static class ServiceRegistration
{
    public static void AddSignalRServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddTransient<IProductHubService, ProductHubService>();
    }
}