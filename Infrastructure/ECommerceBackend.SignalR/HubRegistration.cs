using ECommerceBackend.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;

namespace ECommerceBackend.SignalR;

public static class HubRegistration
{
    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ProductHub>("/products-hub");
    }
}