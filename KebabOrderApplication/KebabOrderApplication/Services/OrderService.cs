using AutoMapper;
using KebabOrderApplication.DTO;
using KebabOrderApplication.Models;
using KebabOrderApplication.Repositories;

namespace KebabOrderApplication.Services;

class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuRepository _menuRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IMenuRepository menuRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _menuRepository = menuRepository;
        _mapper = mapper;
    }
    public async Task<MenuDto> GetCurrentMenu()
    {
        return _mapper.Map<MenuDto>(await _menuRepository.GetCurrentMenu());
    }

    public async Task<OrderResponseDto> Get(Guid id)
    {
        return _mapper.Map<OrderResponseDto>(await _orderRepository.Get(id));
    }

    public async Task<IEnumerable<OrderResponseDto>> GetAll()
    {
        return _mapper.Map<IEnumerable<OrderResponseDto>>(await _orderRepository.GetAll());
    }

    public async Task<Guid> CreateOrder(OrderRequestDto orderRequestDto)
    {
        var id = Guid.NewGuid();
        await _orderRepository.Add(Order.CreateOrder(id, orderRequestDto));

        return id;
    }

    public async Task UpdateOrder(OrderUpdateDto orderRequestDto)
    {
        await _orderRepository.Update(_mapper.Map<Order>(orderRequestDto));
    }

    public async Task DeleteOrder(Guid id)
    {
        await _orderRepository.Delete(id);
    }
}