namespace KebabOrderApplication.Models;

public class Menu
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public IEnumerable<MenuItem> Items { get; set; }
}