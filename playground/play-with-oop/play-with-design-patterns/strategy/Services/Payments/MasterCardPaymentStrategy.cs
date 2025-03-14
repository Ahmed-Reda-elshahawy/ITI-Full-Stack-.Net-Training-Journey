namespace strategy.Services.Payments;

class MasterCardPaymentStrategy:IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing MasterCard payment with amount {amount}$...");
    }
}
