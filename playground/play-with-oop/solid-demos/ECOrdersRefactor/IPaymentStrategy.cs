namespace ECOrdersRefactor;

interface IPaymentStrategy
{
    public void ProcessPayment(double amount);
}
