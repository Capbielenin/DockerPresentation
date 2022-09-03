namespace KebabMenuApplication.DTO;

public record MenuItemUpdateDto(Guid MenuItemId, Guid MenuId, string Name, decimal Price);