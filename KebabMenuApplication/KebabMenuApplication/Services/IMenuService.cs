using KebabMenuApplication.DTO;
using KebabMenuApplication.Models;

namespace KebabMenuApplication.Services;

public interface IMenuService
{
    Task<MenuResponseDto> GetActiveMenu();
    Task SetActiveMenu(Guid id);
    Task<IEnumerable<MenuResponseDto>> GetAllMenus();
    Task<Guid> CreateMenu(MenuRequestDto menuRequestDto);
    Task UpdateMenu(MenuUpdateDto menu);
    Task DeleteMenu(Guid id);
    Task<MenuResponseDto> GetMenu(Guid id);
}