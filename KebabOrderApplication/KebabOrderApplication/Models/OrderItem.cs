namespace KebabOrderApplication.Models;

public class OrderItem
{
    public Guid ItemId { get; set; }
    public Guid MenuItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}