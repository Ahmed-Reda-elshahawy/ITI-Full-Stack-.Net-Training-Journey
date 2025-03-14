namespace strategy.Services.Pricing;

class GoldPricingStrategy : PricingStrategy
{
    protected override double Discount => 0.1;
}
