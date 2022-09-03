namespace KebabMenuApplication.DTO;

public record MenuRequestDto(DateTime CreationDate, IEnumerable<MenuItemRequestDto> MenuItems);