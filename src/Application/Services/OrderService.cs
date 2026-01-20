using RetroConfigurator.Application.Interfaces;
using RetroConfigurator.Domain.Entities;
using RetroConfigurator.Domain.Repositories;
using RetroConfigurator.Domain.ValueObjects;

namespace RetroConfigurator.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Order> CreateOrderAsync(ConsoleConfig configuration, string customerEmail)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Customer email is required", nameof(customerEmail));

        var order = new Order(configuration, customerEmail);
        return await _orderRepository.AddAsync(order);
    }

    public async Task<Order?> GetOrderByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid order ID", nameof(id));

        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        return await _orderRepository.GetByCustomerEmailAsync(email);
    }

    public async Task CompleteOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new InvalidOperationException($"Order with ID {orderId} not found");

        order.MarkAsCompleted();
        await _orderRepository.UpdateAsync(order);
    }

    public async Task CancelOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new InvalidOperationException($"Order with ID {orderId} not found");

        order.Cancel();
        await _orderRepository.UpdateAsync(order);
    }
}
