using RetroConfigurator.Domain.Entities;
using RetroConfigurator.Domain.ValueObjects;

namespace RetroConfigurator.Application.Interfaces;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(ConsoleConfig configuration, string customerEmail);
    Task<Order?> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<IEnumerable<Order>> GetOrdersByCustomerEmailAsync(string email);
    Task CompleteOrderAsync(Guid orderId);
    Task CancelOrderAsync(Guid orderId);
}
