using observer.Models;
using observer.Services.Notifications;
using observer.Services.Offers;
using observer.Services.Products;

namespace observer.Services.Subscriptions;

class OffersSubscriptionService : SubscriptionService<OffersManagementService>
{
    private readonly ProductsManagementService _productsManagementService   ;
    public OffersSubscriptionService(NotificationService notificationService, ProductsManagementService productManagementService) : base(notificationService, SubscriptionAction.OfferCreated)
    {
        _productsManagementService = productManagementService;
    }

    public override void SubscribeToPublisherEvents(OffersManagementService publisher)
    {
        publisher.OfferCreated += (sender, args) =>
        {
            var product = _productsManagementService.GetProductById(args.Offer.ProductId);
            NotifySubscribers(SubscriptionAction.OfferCreated, $"New Offer on {product?.Name} with discount {args.Offer.Discount}%");
        };
    }
}
