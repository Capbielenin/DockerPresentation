using KebabOrderApplication.DTO;
using KebabOrderApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace KebabOrderApplication.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{

    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(
        ILogger<OrdersController> logger,
        IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        return Ok(await _orderService.Get(id));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _orderService.GetAll());
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderRequestDto orderRequest)
    {
        var id = await _orderService.CreateOrder(orderRequest);
        return CreatedAtAction(nameof(Get), id);
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(OrderUpdateDto updateRequest)
    {
        await _orderService.UpdateOrder(updateRequest);
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _orderService.DeleteOrder(id);
        return Ok();
    }

    [HttpGet("/api/menu/current")]
    public async Task<IActionResult> GetMenu()
    {
        return Ok(await _orderService.GetCurrentMenu());
    }
}