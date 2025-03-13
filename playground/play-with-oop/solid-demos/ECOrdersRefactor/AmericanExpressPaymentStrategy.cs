namespace ECOrdersRefactor;

class AmericanExpressPaymentStrategy:IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing american express payment with amount {amount}$...");
    }
}
