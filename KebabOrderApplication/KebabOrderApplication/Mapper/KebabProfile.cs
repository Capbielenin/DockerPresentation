using AutoMapper;
using KebabOrderApplication.DTO;
using KebabOrderApplication.Events;
using KebabOrderApplication.Models;

namespace KebabOrderApplication.Mapper;

public class KebabProfile : Profile
{
    public KebabProfile()
    {
        CreateMap<MenuItem, MenuItemDto>().ReverseMap();
        CreateMap<Menu, MenuDto>();
        
        CreateMap<Order, OrderResponseDto>();
        CreateMap<OrderItem, OrderItemResponseDto>();
        
        CreateMap<OrderUpdateDto, Order>();
        CreateMap<OrderItemUpdateDto, OrderItem>();
        
        CreateMap<NewActiveMenuEvent, Menu>();
        CreateMap<NewActiveMenuEventItem, MenuItem>();
    }
}