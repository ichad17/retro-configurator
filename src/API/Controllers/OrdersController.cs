using Microsoft.AspNetCore.Mvc;
using RetroConfigurator.API.DTOs;
using RetroConfigurator.Application.Interfaces;
using RetroConfigurator.Domain.Enums;
using RetroConfigurator.Domain.ValueObjects;

namespace RetroConfigurator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        try
        {
            var consoleConfig = new ConsoleConfig(
                (ConsoleType)request.ConsoleType,
                request.NumberOfControllers,
                request.HDMISupport,
                request.CustomColor,
                request.ColorHex
            );

            var order = await _orderService.CreateOrderAsync(consoleConfig, request.CustomerEmail);

            var response = new OrderResponse(
                order.Id,
                new ConsoleConfigDto(
                    (int)order.Configuration.ConsoleType,
                    order.Configuration.NumberOfControllers,
                    order.Configuration.HDMISupport,
                    order.Configuration.CustomColor,
                    order.Configuration.ColorHex
                ),
                order.TotalPrice,
                (int)order.Status,
                order.CreatedAt,
                order.CompletedAt,
                order.CustomerEmail
            );

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, response);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid order request");
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order");
            return StatusCode(500, new { error = "An error occurred while creating the order" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponse>> GetOrder(Guid id)
    {
        try
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound(new { error = "Order not found" });

            var response = new OrderResponse(
                order.Id,
                new ConsoleConfigDto(
                    (int)order.Configuration.ConsoleType,
                    order.Configuration.NumberOfControllers,
                    order.Configuration.HDMISupport,
                    order.Configuration.CustomColor,
                    order.Configuration.ColorHex
                ),
                order.TotalPrice,
                (int)order.Status,
                order.CreatedAt,
                order.CompletedAt,
                order.CustomerEmail
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order {OrderId}", id);
            return StatusCode(500, new { error = "An error occurred while retrieving the order" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var response = orders.Select(o => new OrderResponse(
                o.Id,
                new ConsoleConfigDto(
                    (int)o.Configuration.ConsoleType,
                    o.Configuration.NumberOfControllers,
                    o.Configuration.HDMISupport,
                    o.Configuration.CustomColor,
                    o.Configuration.ColorHex
                ),
                o.TotalPrice,
                (int)o.Status,
                o.CreatedAt,
                o.CompletedAt,
                o.CustomerEmail
            ));

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders");
            return StatusCode(500, new { error = "An error occurred while retrieving orders" });
        }
    }

    [HttpGet("customer/{email}")]
    public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByEmail(string email)
    {
        try
        {
            var orders = await _orderService.GetOrdersByCustomerEmailAsync(email);
            var response = orders.Select(o => new OrderResponse(
                o.Id,
                new ConsoleConfigDto(
                    (int)o.Configuration.ConsoleType,
                    o.Configuration.NumberOfControllers,
                    o.Configuration.HDMISupport,
                    o.Configuration.CustomColor,
                    o.Configuration.ColorHex
                ),
                o.TotalPrice,
                (int)o.Status,
                o.CreatedAt,
                o.CompletedAt,
                o.CustomerEmail
            ));

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving orders for customer {Email}", email);
            return StatusCode(500, new { error = "An error occurred while retrieving orders" });
        }
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        try
        {
            await _orderService.CompleteOrderAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation on order {OrderId}", id);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing order {OrderId}", id);
            return StatusCode(500, new { error = "An error occurred while completing the order" });
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        try
        {
            await _orderService.CancelOrderAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Invalid operation on order {OrderId}", id);
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling order {OrderId}", id);
            return StatusCode(500, new { error = "An error occurred while cancelling the order" });
        }
    }
}
