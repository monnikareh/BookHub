using BusinessLayer.Services;
using BusinessLayer.Facades;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer;

public class BusinessLayerServiceExtension
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        // ... Other service registrations
    }

    public static void RegisterFacades(IServiceCollection services)
    {
        services.AddScoped<IOrderFacade, OrderFacade>();
        // ... Other facade registrations
    }
}