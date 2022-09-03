namespace KebabOrderApplication.DTO;

public record OrderItemUpdateDto
{
    public Guid OrderId { get; init; } 
    public Guid MenuItemId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
};