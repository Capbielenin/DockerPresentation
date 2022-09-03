using KebabOrderApplication.DTO;
using Marten.Schema;

namespace KebabOrderApplication.Models;

public class Order
{
    [Identity]
    public Guid OrderId { get; set; }
    public DateTime CreationDate { get; set; }
    
    public List<OrderItem> Items { get; set; }

    public static Order CreateOrder(Guid newOrderId, OrderRequestDto requestDto)
    {
        return new()
        {
            OrderId = newOrderId,
            CreationDate = requestDto.CreationDate,
            Items = requestDto.Items.Select(item => new OrderItem()
            {
                Name = item.Name,
                Price = item.Price,
                ItemId = Guid.NewGuid(),
                MenuItemId = item.Id
            }).ToList()
        };
    }
    
    public static Order CreateOrder(OrderUpdateDto updateDto)
    {
        return new()
        {
            OrderId = updateDto.OrderId,
            CreationDate = updateDto.CreationDate,
            Items = updateDto.Items.Select(item => new OrderItem()
            {
                Name = item.Name,
                Price = item.Price,
                ItemId = Guid.NewGuid()
            }).ToList()
        };
    }
}