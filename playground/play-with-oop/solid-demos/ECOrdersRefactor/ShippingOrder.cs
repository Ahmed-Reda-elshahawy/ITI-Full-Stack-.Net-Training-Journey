namespace ECOrdersRefactor;

class ShippingOrder : Order
{
    private static readonly double ShippingCost = 10.0;
    public override double CalculateTotalPrice()
    {
        return Price + ShippingCost;
    }
}
