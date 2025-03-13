namespace ECOrdersRefactor;

class PaypalPaymentStrategy:IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing paypal payment with amount {amount}$...");
    }
}
