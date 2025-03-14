namespace strategy.Services.Payments;

class VisaPaymentStrategy : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing Visa Card payment with amount {amount}$...");
    }
}
