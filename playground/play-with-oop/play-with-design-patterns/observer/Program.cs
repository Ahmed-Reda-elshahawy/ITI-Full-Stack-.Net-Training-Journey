// Dependancies
using observer.Services.Notifications;
using observer.Services.Offers;
using observer.Services.Products;
using observer.Services.Subscriptions;
using observer.Services.Users;
using observer.Models;

// Create Services Instances
UsersManagementService usersManagementService = new();
ProductsManagementService  productsManagementService = new();
OffersManagementService offersManagementService = new();
NotificationService notificationService = new(new ConsoleNotificationStrategy());
ProductsSubscriptionService productsSubscriptionService = new(notificationService);
OffersSubscriptionService offersSubscriptionService = new(notificationService, productsManagementService);


// Register Publisher services to Subscription Services
productsSubscriptionService.SubscribeToPublisherEvents(productsManagementService);
offersSubscriptionService.SubscribeToPublisherEvents(offersManagementService);


// user subscripe to product
foreach (var user in usersManagementService.GetAllUsers())
{
    productsSubscriptionService.Subscribe(user, SubscriptionAction.ProductCreated);
}

offersSubscriptionService.Subscribe(usersManagementService.GetUserById(1), SubscriptionAction.OfferCreated);

productsManagementService.CreateProduct(new Product { Name = "Apple Watch", Price = 500 });
offersManagementService.CreateOffer(new Offer { ProductId = 1, Discount = 10 });