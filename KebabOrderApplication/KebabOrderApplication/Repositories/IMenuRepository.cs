using KebabOrderApplication.DTO;
using KebabOrderApplication.Models;

namespace KebabOrderApplication.Repositories;

public interface IMenuRepository
{
    Task<Menu> GetCurrentMenu();
    Task AddMenu(Menu menu);
}