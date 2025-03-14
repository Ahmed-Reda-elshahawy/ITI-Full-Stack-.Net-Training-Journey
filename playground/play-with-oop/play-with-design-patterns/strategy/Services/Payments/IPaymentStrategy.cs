namespace strategy.Services.Payments;

interface IPaymentStrategy
{
    void ProcessPayment(double amount);
}
