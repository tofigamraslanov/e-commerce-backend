using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}