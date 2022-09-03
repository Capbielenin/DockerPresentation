using AutoMapper;
using KebabMenuApplication.DTO;
using KebabMenuApplication.Events;
using KebabMenuApplication.Extensions;
using KebabMenuApplication.Models;
using KebabMenuApplication.Repositories;

namespace KebabMenuApplication.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<MenuService> _logger;
    private readonly IBusPublisher _busPublisher;

    public MenuService(
        IMenuRepository repository,
        IBusPublisher busPublisher,
        IMapper mapper,
        ILogger<MenuService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _busPublisher = busPublisher;
    }

    public async Task<MenuResponseDto> GetActiveMenu()
    {
        try
        {
            return _mapper.Map<MenuResponseDto>(await _repository.GetActiveMenu());
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(GetActiveMenu)}");
            throw;
        }
    }

    public async Task SetActiveMenu(Guid id)
    {
        try
        {
            await _repository.SetActiveMenu(id);

            _busPublisher.PublishAsync(NewActiveMenuEvent.Create(await _repository.Get(id)));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(SetActiveMenu)}");
            throw;
        }
    }

    public async Task<IEnumerable<MenuResponseDto>> GetAllMenus()
    {
        try
        {
            return _mapper.Map<IEnumerable<MenuResponseDto>>(await _repository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(GetAllMenus)}");
            throw;
        }
    }

    public async Task<Guid> CreateMenu(MenuRequestDto menuResponseDto)
    {
        try
        {
            var menuId = Guid.NewGuid();
            Menu menu = new(
                menuId,
                menuResponseDto.MenuItems.Select(dt =>
                    new MenuItem(Guid.NewGuid(), menuId, dt.Name, dt.Price)).ToList(),
                menuResponseDto.CreationDate);

            await _repository.Create(menu);

            return menuId;        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(CreateMenu)}");
            throw;
        }
    }

    public async Task UpdateMenu(MenuUpdateDto menu)
    {
        try
        {
            await _repository.Update(_mapper.Map<Menu>(menu));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(UpdateMenu)}");
            throw;
        }
    }

    public async Task DeleteMenu(Guid id)
    {
        try
        {
            await _repository.Delete(id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(UpdateMenu)}");
            throw;
        }
    }

    public async Task<MenuResponseDto> GetMenu(Guid id)
    {
        try
        {
            return _mapper.Map<MenuResponseDto>(await _repository.Get(id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Problem during {nameof(UpdateMenu)}");
            throw;
        }
    }
}