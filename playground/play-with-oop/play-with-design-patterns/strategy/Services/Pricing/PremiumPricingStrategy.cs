namespace strategy.Services.Pricing;

class PremiumPricingStrategy : PricingStrategy
{
    protected override double Discount => 0.2;
}
