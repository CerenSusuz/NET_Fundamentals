using Microsoft.AspNetCore.Mvc;
using ORMFundamentals.Entities;
using ORMFundamentals.Services;

namespace ORMFundamentals.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet]
    public IEnumerable<Order> GetAll()
    {
        return _orderService.GetAllOrders();
    }

    [HttpGet("{id}")]
    public ActionResult<Order> GetOrderById(int id)
    {
       return this._orderService.GetOrderById(id) ?? new ActionResult<Order>(NotFound());
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Add(Order order)
    {
        await _orderService.Add(order);

        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        await _orderService.Update(order);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.Delete(id);

        return NoContent();
    }
}
