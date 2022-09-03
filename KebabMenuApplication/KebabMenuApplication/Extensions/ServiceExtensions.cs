using KebabMenuApplication.Repositories;
using KebabMenuApplication.Repositories.Factories;
using KebabMenuApplication.Services;

namespace KebabMenuApplication.Extensions;

public static class ServiceExtensions
{
    public static void AddDataServices(this IServiceCollection col)
    {
        col.AddScoped<IMenuRepository, MenuRepository>();
        col.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        col.AddTransient<IMenuService, MenuService>();

    }
    
    public static void AddMessageBus(this IServiceCollection col)
    {
        col.AddTransient<IBusPublisher, BusPublisher>();
    }
}