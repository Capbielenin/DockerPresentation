using Dapper;
using Dapper.Contrib.Extensions;
using KebabMenuApplication.Models;
using KebabMenuApplication.Repositories.DbQueries;
using KebabMenuApplication.Repositories.Factories;

namespace KebabMenuApplication.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<MenuRepository> _logger;

    public MenuRepository(
        IDbConnectionFactory connectionFactory,
        ILogger<MenuRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }
    
    public async Task<Menu> GetActiveMenu()
    {
        using var connection = _connectionFactory.GetConnection();
        
        try
        {
            _logger.LogInformation("Getting active menu");
            var menu = 
                await connection.QuerySingleAsync<Menu>(DatabaseQueries.GetActiveMenu);
            var items = 
                await connection.QueryAsync<MenuItem>(DatabaseQueries.GetMenuItemsByMenuId, new { id = menu.MenuId });
            menu.MenuItems = items;
            _logger.LogInformation("Finish Getting active menu");

            return menu;

        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);
            throw;
        }

    }

    public async Task SetActiveMenu(Guid id)
    {
        using var connection = _connectionFactory.GetConnection();
        try
        {
            _logger.LogInformation("Setting active menu");

            await connection.QueryAsync<Menu>(DatabaseQueries.SetActiveMenu, new { id });
            
            _logger.LogInformation("Finish Setting active menu");

        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Menu>> GetAll()
    {
        
        using var connection = _connectionFactory.GetConnection();
        _logger.LogInformation("Getting All Menus");

        try
        {
            List<Menu> menus =
                (await connection.QueryAsync<Menu>(DatabaseQueries.GetAllMenus)).ToList();
            IEnumerable<MenuItem>? items = 
                await connection.QueryAsync<MenuItem>(DatabaseQueries.GetAllMenuItems);
        
            menus.ForEach(m => m.MenuItems = items.Where(i => i.MenuId == m.MenuId));

            _logger.LogInformation("Finish Getting All Menus");

            return menus;
        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);

            throw;
        }
    }

    public async Task Create(Menu menu)
    {
        using var connection = _connectionFactory.GetConnection();

        try
        {
            _logger.LogInformation($"Creating menu with id {menu.MenuId}, date {menu.CreationDate}");
            
            await connection.ExecuteAsync(DatabaseQueries.CreateMenu, menu);
            await connection.ExecuteAsync(DatabaseQueries.CreateItemsByMenuId, menu.MenuItems);
            
            _logger.LogInformation($"Finish Creating menu with id {menu.MenuId}, date {menu.CreationDate}");

        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);

            throw;
        }
    }
    
    public async Task Update(Menu menu)
    {
        using var connection = _connectionFactory.GetConnection();
        _logger.LogInformation($"Updating menu with id {menu.MenuId}, date {menu.CreationDate}");

        try
        {
            await connection.ExecuteAsync(DatabaseQueries.DeleteItemsByMenuId, new { id = menu.MenuId });
            await connection.ExecuteAsync(DatabaseQueries.CreateItemsByMenuId, menu.MenuItems);

            await connection.UpdateAsync<Menu>(menu);
            
            _logger.LogInformation($"Finish Updating menu with id {menu.MenuId}, date {menu.CreationDate}");
        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);

            throw;
        }
        
    }

    public async Task Delete(Guid id)
    {
        using var connection = _connectionFactory.GetConnection();

        try
        {
            _logger.LogInformation($"Deleting menu with id {id}");
            await connection.QueryAsync<Menu>(DatabaseQueries.DeleteItemsByMenuId, new { id });
            await connection.QueryAsync<Menu>(DatabaseQueries.DeleteMenu, new { id });
            _logger.LogInformation($"Finish Deleting menu with id {id}");

        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);

            throw;
        }
        
    }

    public async Task<Menu> Get(Guid id)
    {
        using var connection = _connectionFactory.GetConnection();

        try
        {
            _logger.LogInformation($"Getting menu with id {id}, date");

            var items = await connection.QueryAsync<MenuItem>(DatabaseQueries.GetMenuItemsByMenuId, new { id });
            var menu = await connection.QuerySingleAsync<Menu>(DatabaseQueries.GetMenusById, new { id });

            menu.MenuItems = items;
            _logger.LogInformation($"Finish Getting menu with id {id}, date");

            return menu;
        }
        catch (Exception e)
        {
            connection.Close();
            _logger.LogError(e, e.Message);

            throw;
        }
    }
}