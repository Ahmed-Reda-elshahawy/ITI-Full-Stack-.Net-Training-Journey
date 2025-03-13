namespace ECOrdersRefactor;

class OrderManagmentService
{
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Processing order: {order.Id} now...");
    }
}
