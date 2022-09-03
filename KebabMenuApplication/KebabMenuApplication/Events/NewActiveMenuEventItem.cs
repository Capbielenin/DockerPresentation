namespace KebabMenuApplication.Events;

public class NewActiveMenuEventItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}