namespace KebabMenuApplication.DTO;

public record MenuResponseDto(Guid MenuId, DateTime CreationDate, IEnumerable<MenuItemResponseDto> MenuItems);