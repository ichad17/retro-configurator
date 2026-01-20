using Stripe;

namespace RetroConfigurator.Infrastructure.Payment;

public class StripePaymentService : IPaymentService
{
    private readonly string _apiKey;

    public StripePaymentService(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("Stripe API key is required", nameof(apiKey));

        _apiKey = apiKey;
        StripeConfiguration.ApiKey = _apiKey;
    }

    public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency = "usd")
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100), // Stripe expects amount in cents
            Currency = currency,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        };

        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);

        return paymentIntent.Id;
    }

    public async Task<bool> ProcessPaymentAsync(string paymentIntentId)
    {
        var service = new PaymentIntentService();
        var paymentIntent = await service.GetAsync(paymentIntentId);

        return paymentIntent.Status == "succeeded";
    }
}
