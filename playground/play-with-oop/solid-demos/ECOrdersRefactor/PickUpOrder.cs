namespace ECOrdersRefactor;

class PickUpOrder : Order
{
    public override double CalculateTotalPrice()
    {
        return Price;
    }
}
