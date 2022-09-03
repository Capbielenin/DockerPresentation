using KebabOrderApplication.DTO;

namespace KebabOrderApplication.Services;

public interface IOrderService
{
    Task<MenuDto> GetCurrentMenu();
    Task<OrderResponseDto> Get(Guid id);
    Task<IEnumerable<OrderResponseDto>> GetAll();
    Task<Guid> CreateOrder(OrderRequestDto orderRequestDto);
    Task UpdateOrder(OrderUpdateDto orderRequestDto);
    Task DeleteOrder(Guid id);
}