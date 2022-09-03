namespace KebabOrderApplication.Events;

public class NewActiveMenuEvent
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public IEnumerable<NewActiveMenuEventItem> Items { get; set; }
    
}