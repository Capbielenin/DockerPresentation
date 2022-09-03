using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace KebabMenuApplication.Models
{
    [Table("Menu")]
    public class Menu
    {
        [ExplicitKey]
        public Guid MenuId { get; set; }
        public DateTime CreationDate { get; set; }
        [Computed]
        public IEnumerable<MenuItem> MenuItems { get; set; }

        public Menu()
        {

        }
        
        public Menu(Guid id, List<MenuItem> items, DateTime creationDate)
        {
            MenuId = id;
            MenuItems = items;
            CreationDate = creationDate;
        }
    }
}
