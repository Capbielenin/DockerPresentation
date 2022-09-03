using KebabOrderApplication.Models;
using Marten;

namespace KebabOrderApplication.Repositories;

class MenuRepository : IMenuRepository
{
    private readonly IDocumentStore _store;
    private readonly ILogger<MenuRepository> _logger;

    public MenuRepository(
        IDocumentStore store,
        ILogger<MenuRepository> logger)
    {
        _store = store;
        _logger = logger;
    }
    public async Task<Menu> GetCurrentMenu()
    {
        _logger.LogInformation("Getting current menu...");
        await using var session = _store.LightweightSession();
        try
        {
            return await session.Query<Menu>()
                .OrderByDescending(m => m.CreationDate).FirstAsync();
        } 
        catch (Exception e)
        {
            session.Dispose();
            _logger.LogError(e, "Problem during getting the current menu");
            throw;
        }
    }

    public async Task AddMenu(Menu menu)
    {
        _logger.LogInformation($"Adding new menu with id {menu.Id}");

        await using var session = _store.LightweightSession();

        try
        {
            session.Store(menu);
            await session.SaveChangesAsync();
            _logger.LogInformation($"Finish adding new menu with id {menu.Id}");

        }
        catch (Exception e)
        {
            
            session.Dispose();
            _logger.LogError(e, "Problem during adding the current menu");
            throw;
        }


    }
}