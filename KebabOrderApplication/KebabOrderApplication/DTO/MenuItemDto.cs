namespace KebabOrderApplication.DTO;

public record MenuItemDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
};