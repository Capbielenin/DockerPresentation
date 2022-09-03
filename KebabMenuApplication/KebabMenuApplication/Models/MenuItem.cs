using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace KebabMenuApplication.Models
{
    public class MenuItem
    {
        [ExplicitKey]
        public Guid MenuItemId { get; set; }
        public Guid MenuId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public MenuItem() {  }
        
        public MenuItem(Guid menuItemId, Guid menuId, string name, decimal price)
        {
            MenuItemId = menuItemId;
            MenuId = menuId;
            Name = name;
            Price = price;
        }   
    }
}