using KebabOrderApplication.Models;

namespace KebabOrderApplication.Repositories;

public interface IOrderRepository
{
    public Task<IEnumerable<Order>> GetAll();
    public Task<Order> Get(Guid id);
    public Task Add(Order order);
    public Task Update(Order order);
    public Task Delete(Guid id);
}