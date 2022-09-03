using KebabMenuApplication.DTO;
using KebabMenuApplication.Models;
using KebabMenuApplication.Options;
using KebabMenuApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static KebabMenuApplication.ErrorMessages.ErrorMessages;

namespace KebabMenuApplication.Controllers;

[ApiController]
[Route("api/menu")]
public class MenuController : ControllerBase
{
    private readonly ILogger<MenuController> _logger;
    private readonly IOptions<ConnectionOptions> _conn;
    private readonly IMenuService _service;

    public MenuController(ILogger<MenuController> logger, IOptions<ConnectionOptions> conn, IMenuService service)
    {
        _logger = logger;
        _conn = conn;
        _service = service;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var menus = await _service.GetAllMenus();
            return Ok(menus);        
        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery]Guid id)
    {
        try
        {
            var menus = await _service.GetMenu(id);
            return Ok(menus);      
        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
    
    [HttpPost]
    public async  Task<IActionResult> Post([FromBody] MenuRequestDto newMenuResponse)
    { 
        try
        {
            var id = await _service.CreateMenu(newMenuResponse);
        
            return CreatedAtAction(nameof(Get), new { id });        
        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
    
    [HttpPut]
    public async  Task<IActionResult> Put([FromBody] MenuUpdateDto newMenuResponse)
    {
        try
        {
            await _service.UpdateMenu(newMenuResponse);
            
            return Ok();

        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
    
    [HttpDelete]
    public async  Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _service.DeleteMenu(id);
        
            return Ok();

        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
    
    [HttpGet("active")]
    public async  Task<IActionResult> GetActive()
    {
        try
        {
            return Ok(await _service.GetActiveMenu());
        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }

    [HttpPut("active")]
    public async  Task<IActionResult> PutActive(Guid id)
    {
        try
        {
            await _service.SetActiveMenu(id);
        
            return Ok();
            
        }
        catch
        {
            return Problem(SERVER_ERROR_MESSAGE);
        }
    }
}
