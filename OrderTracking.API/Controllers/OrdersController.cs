using Microsoft.AspNetCore.Mvc;
using OrderTracking.Business.Services.Abstarct;
using OrderTracking.Core.DTOs.Order;
using OrderTracking.Core.Enums;

namespace OrderTracking.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var result = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _orderService.GetOrdersWithPaginationAsync(page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);
        return Ok(result);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderUpdateStatusDto dto)
    {
        await _orderService.UpdateOrderStatusAsync(id, (OrderStatus)dto.Status);
        return NoContent();
    }
}