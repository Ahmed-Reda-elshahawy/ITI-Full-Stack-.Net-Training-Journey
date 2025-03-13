namespace ECOrdersRefactor;

class MasterCardPaymentStrategy:IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing mastercard card payment with amount {amount}$...");
    }
}
