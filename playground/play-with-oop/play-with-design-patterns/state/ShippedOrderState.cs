namespace state;

class ShippedOrderState : IOrderState
{
    public string GetStatus()
    {
        return "Shipped";
    }

    public void Process(Order order)
    {
        Console.WriteLine("order is already processed!");
    }

    public void Ship(Order order)
    {
        Console.WriteLine("Order is already shipped!");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Cannot delete already shipped order!");
    }
}
