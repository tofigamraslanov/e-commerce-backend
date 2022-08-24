using System.Reflection;
using ECommerceBackend.Application.Options.ExternalLogin;
using ECommerceBackend.Application.Options.Token;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceBackend.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddHttpClient();

        services.Configure<TokenOptions>(configuration.GetSection(TokenOptions.SectionName));
        services.Configure<FacebookLoginOptions>(configuration.GetSection(FacebookLoginOptions.SectionName));
        services.Configure<GoogleLoginOptions>(configuration.GetSection(GoogleLoginOptions.SectionName));
    }
}