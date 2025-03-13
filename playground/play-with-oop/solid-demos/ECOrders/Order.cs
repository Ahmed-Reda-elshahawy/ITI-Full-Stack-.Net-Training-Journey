namespace ECOrders;

class Order
{
    private static readonly double ShippingCost = 10.0;
    public int Id { get; set; }
    public double Price { get; set; }
    public double TotalPrice { get => Price + ShippingCost; }
}
