namespace ECOrdersRefactor;

class PaymentProcessingService
{
    private readonly IPaymentStrategy _paymentStrategy;
    public PaymentProcessingService(IPaymentStrategy paymentStrategy)
    {
        this._paymentStrategy = paymentStrategy;
    }
    public void ProcessPayment(Order order)
    {
        Console.WriteLine($"Processing payments of order {order.Id}");

        Console.WriteLine($"Issuing payment for amount: {order.CalculateTotalPrice()}");

        _paymentStrategy.ProcessPayment(order.CalculateTotalPrice());
    }
}
