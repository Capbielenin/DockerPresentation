using KebabMenuApplication.DTO;
using KebabMenuApplication.Models;

namespace KebabMenuApplication.Repositories;

public interface IMenuRepository
{
    Task<Menu> GetActiveMenu();
    Task SetActiveMenu(Guid id);
    Task<IEnumerable<Menu>> GetAll();
    Task Create(Menu menu);
    Task Update(Menu menu);
    Task Delete(Guid id);
    Task<Menu> Get(Guid id);
}