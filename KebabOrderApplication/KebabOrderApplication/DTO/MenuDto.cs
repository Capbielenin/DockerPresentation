namespace KebabOrderApplication.DTO;

public record MenuDto
{
    public Guid Id { get; init; }
    public DateTime CreationDate { get; init; }
    public IEnumerable<MenuItemDto> Items { get; init; }
};