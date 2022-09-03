namespace KebabOrderApplication.DTO;

public record OrderRequestDto
{
    public DateTime CreationDate { get; init; }
    public IEnumerable<MenuItemDto> Items { get; init; }
};