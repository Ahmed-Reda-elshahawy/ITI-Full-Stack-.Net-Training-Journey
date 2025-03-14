namespace strategy.Services.Pricing;

abstract class PricingStrategy
{
    protected abstract double Discount { get; }

    public virtual double CalculatePrice(double price)
    {
        return price * (1 - Discount);
    }
}
