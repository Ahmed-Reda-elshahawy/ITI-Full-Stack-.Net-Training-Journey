namespace strategy.Services.Payments;

class BankTransferPaymentStrategy:IPaymentStrategy
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Processing Bank Transfer payment with amount {amount}$...");
    }
}
