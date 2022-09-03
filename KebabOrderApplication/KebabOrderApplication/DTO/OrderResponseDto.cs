namespace KebabOrderApplication.DTO;

public record OrderResponseDto
{
    public Guid OrderId { get; init; }
    public DateTime CreationDate { get; init; }
    public IEnumerable<OrderItemResponseDto> Items { get; init; }
};