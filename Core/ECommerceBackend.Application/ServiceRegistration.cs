using System.Reflection;
using ECommerceBackend.Application.Options.Token;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.Token));

        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}