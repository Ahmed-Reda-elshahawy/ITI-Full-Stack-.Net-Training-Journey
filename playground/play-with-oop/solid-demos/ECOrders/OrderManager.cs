namespace ECOrders;

class OrderManager
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Processing order: {order.Id} now...");
    }

    public void ProcessPayment(Order order, Payment payment)
    {
        Console.WriteLine($"Processing payments of order {order.Id}");

        Console.WriteLine($"Issuing payment for amount: {order.TotalPrice}");


        if (payment.Type == "VISA")
        {
            Console.WriteLine("Processing visa card payments...");
        } else if (payment.Type ==  "MASTER_CARD")
        {
            Console.WriteLine("Processing master card payments...");
        } else if (payment.Type == "AMERICAN_EXPRESS")
        {
            Console.WriteLine("Processing american express card payments...");
        } else
        {
            throw new InvalidOperationException("Un supported payment....");
        }
    }

    public void SendEmailNotification(Customer customer, string message)
    {
        Console.WriteLine($"Sending email notification to: {customer.Name} with message {message}.");
    }
}
