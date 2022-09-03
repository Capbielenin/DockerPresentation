using System.Data.Common;
using KebabOrderApplication.Models;
using Marten;
using Marten.Linq;

namespace KebabOrderApplication.Repositories;

class OrderRepository : IOrderRepository
{
    private readonly IDocumentStore _store;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(
        IDocumentStore store,
        ILogger<OrderRepository> logger)
    {
        _store = store;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Order>> GetAll()
    {
        await using var session = _store.LightweightSession();
        try
        {
            return await session.Query<Order>().ToListAsync();

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Problem during getting all orders");
            throw;
        }
    }

    public async Task<Order> Get(Guid id)
    {
        await using var session = _store.LightweightSession();
        try
        {
            return await session.Query<Order>()
                .FirstAsync(o => o.OrderId == id);    
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during getting order {id}");
            throw;
        }
        
    }

    public async Task Add(Order order)
    {
        
        await using var session = _store.LightweightSession();
        try
        {
            session.Store(order);
            await session.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during adding order {order.OrderId}");
            throw;
        }
    }

    public async Task Update(Order order)
    {
        await using var session = _store.LightweightSession();
        try
        {
            session.Update<Order>(order);
            await session.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem deleting adding order {order.OrderId}");
            throw;
        }    
    }

    public async Task Delete(Guid id)
    {
        await using var session = _store.LightweightSession();
        try
        {
            session.Delete<Order>(id);
            await session.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem deleting adding order {id}");
            throw;
        }
    }
}