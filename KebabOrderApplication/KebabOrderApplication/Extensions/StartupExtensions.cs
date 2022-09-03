using KebabOrderApplication.Options;
using KebabOrderApplication.Repositories;
using KebabOrderApplication.Services;

namespace KebabOrderApplication.Extensions;

public static class StartupExtensions
{
    public static void AddRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<IOrderRepository, OrderRepository>();
        collection.AddTransient<IMenuRepository, MenuRepository>();
    }

    public static void AddServices(this IServiceCollection collection)
    {
        collection.AddHostedService<BusReceiver>();
        collection.AddTransient<IOrderService, OrderService>();
        collection.AddTransient<IMessageHandler, MessageHandler>();
    }
    
}