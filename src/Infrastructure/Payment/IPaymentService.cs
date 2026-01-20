namespace RetroConfigurator.Infrastructure.Payment;

public interface IPaymentService
{
    Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "usd");
    Task<bool> ProcessPaymentAsync(string paymentIntentId);
}
