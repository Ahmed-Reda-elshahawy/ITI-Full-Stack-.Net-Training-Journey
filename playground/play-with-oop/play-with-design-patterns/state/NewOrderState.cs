namespace state;

class NewOrderState : IOrderState
{
    public string GetStatus()
    {
        return "New";
    }

    public void Process(Order order)
    {
        Console.WriteLine("Order is processed successfully!");
        order.OrderState = new ProcessedOrderState();
    }

    public void Ship(Order order)
    {
        Console.WriteLine("New Order cannot be shipped directly!");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Order is deleted successfully!");
        order.OrderState = new CancelledOrderState();
    }
}
