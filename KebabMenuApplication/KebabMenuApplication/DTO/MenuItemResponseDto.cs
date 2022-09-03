namespace KebabMenuApplication.DTO;

public record MenuItemResponseDto(Guid MenuItemId, Guid MenuId, string Name, decimal Price);