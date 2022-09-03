using System.Text.Json;
using AutoMapper;
using KebabOrderApplication.Events;
using KebabOrderApplication.Models;
using KebabOrderApplication.Repositories;

namespace KebabOrderApplication.Services;

public class MessageHandler : IMessageHandler
{
    private readonly IMenuRepository _menuRepository;
    private readonly IMapper _mapper;

    public MessageHandler(
        IMenuRepository menuRepository,
        IMapper mapper)
    {
        _menuRepository = menuRepository;
        _mapper = mapper;
    }
    
    public async Task HandleMessage(string message)
    {
        var @event = JsonSerializer.Deserialize<NewActiveMenuEvent>(message);
        await _menuRepository.AddMenu(_mapper.Map<Menu>(@event));    
    }
}