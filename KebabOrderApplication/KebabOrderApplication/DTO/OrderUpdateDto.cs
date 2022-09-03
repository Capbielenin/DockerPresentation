namespace KebabOrderApplication.DTO;

public record OrderUpdateDto
{
    public Guid OrderId { get; init; }
    public DateTime CreationDate { get; init; }
    public IEnumerable<OrderItemUpdateDto> Items { get; init; }
};