using KebabMenuApplication.Models;

namespace KebabMenuApplication.Events;

public class NewActiveMenuEvent
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public IEnumerable<NewActiveMenuEventItem> Items { get; set; }

    public static NewActiveMenuEvent Create(Menu menu) =>
        new()
        {
            Id = menu.MenuId,
            CreationDate = DateTime.Now,
            Items = menu.MenuItems.Select(i => 
                new NewActiveMenuEventItem { Id = i.MenuItemId, Name = i.Name, Price = i.Price })
        };
}