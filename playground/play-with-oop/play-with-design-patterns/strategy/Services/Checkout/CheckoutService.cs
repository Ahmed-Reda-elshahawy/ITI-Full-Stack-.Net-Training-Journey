using strategy.Models;
using strategy.Services.Payments;

namespace strategy.Services.Checkout;

class CheckoutService(PaymentProcessingService _paymentProcessingService)
{
    public void Checkout(List<Product> order)
    {
        Console.WriteLine("Initiate Checkout Process...");
        var totPrice = order.Select(p => p.PricingStrategy.CalculatePrice(p.Price)).Aggregate((a, b) => a + b);

        _paymentProcessingService.ProcessPayment(totPrice);
        Console.WriteLine($"Completed Order successfully with total amount {totPrice}$");

    }
}
