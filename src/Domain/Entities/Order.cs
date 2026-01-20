using RetroConfigurator.Domain.Enums;
using RetroConfigurator.Domain.ValueObjects;

namespace RetroConfigurator.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public ConsoleConfig Configuration { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string CustomerEmail { get; private set; }

    private Order()
    {
        // EF Core constructor
        Configuration = null!;
        CustomerEmail = null!;
    }

    public Order(ConsoleConfig configuration, string customerEmail)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        if (string.IsNullOrWhiteSpace(customerEmail))
            throw new ArgumentException("Customer email is required", nameof(customerEmail));

        if (!IsValidEmail(customerEmail))
            throw new ArgumentException("Invalid email format", nameof(customerEmail));

        Id = Guid.NewGuid();
        Configuration = configuration;
        CustomerEmail = customerEmail;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        TotalPrice = CalculateTotalPrice();
    }

    public void MarkAsCompleted()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Order is already completed");

        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Cannot complete a cancelled order");

        Status = OrderStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Cannot cancel a completed order");

        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled");

        Status = OrderStatus.Cancelled;
    }

    private decimal CalculateTotalPrice()
    {
        decimal basePrice = Configuration.ConsoleType switch
        {
            ConsoleType.NES => 199.99m,
            ConsoleType.SNES => 249.99m,
            ConsoleType.Genesis => 229.99m,
            ConsoleType.N64 => 299.99m,
            ConsoleType.PlayStation => 349.99m,
            _ => throw new ArgumentException("Unknown console type")
        };

        decimal colorPremium = Configuration.CustomColor ? 29.99m : 0m;
        decimal controllersCost = Configuration.NumberOfControllers * 39.99m;
        decimal hdmiCost = Configuration.HDMISupport ? 49.99m : 0m;

        return basePrice + colorPremium + controllersCost + hdmiCost;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
