namespace KebabOrderApplication.DTO;

public record OrderItemResponseDto
{
    public Guid OrderId { get; set; }
    public Guid MenuItemId {get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
};