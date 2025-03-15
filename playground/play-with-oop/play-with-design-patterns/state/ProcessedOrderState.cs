namespace state;

class ProcessedOrderState : IOrderState
{
    public string GetStatus()
    {
        return "Processed";
    }

    public void Process(Order order)
    {
        Console.WriteLine("order is already processed!");
    }

    public void Ship(Order order)
    {
        Console.WriteLine("Order is shipped successfully!");
        order.OrderState = new ShippedOrderState();
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Order is deleted successfully!");
        order.OrderState = new CancelledOrderState();
    }
}
