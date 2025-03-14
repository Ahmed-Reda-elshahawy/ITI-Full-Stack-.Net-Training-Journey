using strategy.Models;
using strategy.Services.Checkout;
using strategy.Services.Payments;
using strategy.Services.Pricing;

List<Product> products = [
    new Product {Name = "IPhone 16 pro max", Price = 1500, PricingStrategy = new PremiumPricingStrategy()},
    new Product {Name = "Macbook Air", Price = 900, PricingStrategy = new GoldPricingStrategy()},
    new Product {Name = "Apple watch", Price = 600, PricingStrategy = new RegularPricingStrategy()}
    ];

IPaymentStrategy visaPayment = new VisaPaymentStrategy();
PaymentProcessingService paymentProcessingService = new PaymentProcessingService(visaPayment);
CheckoutService checkoutService = new CheckoutService(paymentProcessingService);

checkoutService.Checkout(products);