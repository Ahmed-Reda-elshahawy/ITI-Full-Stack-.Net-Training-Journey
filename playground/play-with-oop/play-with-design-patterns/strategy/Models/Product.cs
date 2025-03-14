using strategy.Services.Pricing;

namespace strategy.Models;

class Product
{
    static int _identitySequence = 0;

    public int Id { get; } = ++_identitySequence;
    public string Name { get; set; }
    public double Price { get; set; }
    public PricingStrategy PricingStrategy { get; set;}
}
