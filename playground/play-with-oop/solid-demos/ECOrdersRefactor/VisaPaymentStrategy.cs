namespace ECOrdersRefactor;

class VisaPaymentStrategy : IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing visa card payment with amount {amount}$...");
    }
}
