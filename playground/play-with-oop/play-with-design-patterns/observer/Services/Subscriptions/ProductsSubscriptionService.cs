using observer.Models;
using observer.Services.Notifications;
using observer.Services.Products;

namespace observer.Services.Subscriptions;

class ProductsSubscriptionService : SubscriptionService<ProductsManagementService>
{
    public ProductsSubscriptionService(NotificationService notificationService)
        : base(notificationService, SubscriptionAction.ProductCreated) { }

    public override void SubscribeToPublisherEvents(ProductsManagementService publisher)
    {
        publisher.ProductCreated += (sender, args) =>
        {
            NotifySubscribers(SubscriptionAction.ProductCreated,
                $"New Product Added: {args.Product.Name}, Price: {args.Product.Price}$");
        };
    }
}
