using AutoMapper;
using KebabMenuApplication.DTO;
using KebabMenuApplication.Models;

namespace KebabMenuApplication.Mappings;

public class KebabMenuMappings : Profile
{
    public KebabMenuMappings()
    {
        CreateMap<Menu, MenuResponseDto>();
        CreateMap<MenuItem, MenuItemResponseDto>();

        CreateMap<MenuUpdateDto, Menu>();
        CreateMap<MenuItemUpdateDto, MenuItem>();
    }
}