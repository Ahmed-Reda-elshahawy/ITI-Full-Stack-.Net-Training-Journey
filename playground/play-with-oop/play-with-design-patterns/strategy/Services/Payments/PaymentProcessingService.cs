namespace strategy.Services.Payments;

class PaymentProcessingService(IPaymentStrategy _paymentStrategy)
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine("Initiate payment process...");
        _paymentStrategy.ProcessPayment(amount);
    }
}
