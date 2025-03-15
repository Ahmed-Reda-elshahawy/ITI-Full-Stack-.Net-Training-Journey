namespace state;

class CancelledOrderState : IOrderState
{
    public string GetStatus()
    {
        return "Cancelled";
    }

    public void Process(Order order)
    {
        Console.WriteLine("Cannot process cancelled order!");
    }

    public void Ship(Order order)
    {
        Console.WriteLine("Cannot ship cancelled order!");
    }

    public void Cancel(Order order)
    {
        Console.WriteLine("Order is already cancelled!");
    }
}
