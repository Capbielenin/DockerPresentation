namespace KebabMenuApplication.DTO;

public record MenuUpdateDto(Guid MenuId, DateTime CreationDate, IEnumerable<MenuItemUpdateDto> MenuItems);