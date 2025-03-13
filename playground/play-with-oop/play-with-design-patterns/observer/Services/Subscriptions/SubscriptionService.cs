using observer.Models;
using observer.Services.Notifications;

namespace observer.Services.Subscriptions;

abstract class SubscriptionService<TPublisher>
    where TPublisher : class
{
    private readonly NotificationService _notificationService;
    private readonly List<User> _subscribers;
    protected readonly List<SubscriptionAction> _actions;

    public SubscriptionService(NotificationService notificationService, params SubscriptionAction[] actions)
    {
        _notificationService = notificationService;
        _actions = actions.ToList();
        _subscribers = new();
    }

    public void Subscribe(User user, SubscriptionAction action)
    {
        if (_actions.Contains(action) && !_subscribers.Contains(user))
        {
            _subscribers.Add(user);
            user.SubscribedActions.Add(action);
            Console.WriteLine($"{user.Name} subscribed to {action} notifications.");
        }
    }

    public void Unsubscribe(User user, SubscriptionAction action)
    {
        if (_subscribers.Contains(user) && user.SubscribedActions.Contains(action))
        {
            _subscribers.Remove(user);
            user.SubscribedActions.Remove(action);
            Console.WriteLine($"{user.Name} unsubscribed from {action} notifications.");
        }
    }

    protected void NotifySubscribers(SubscriptionAction action, string message)
    {
        foreach (var user in _subscribers)
        {
            if (user.SubscribedActions.Contains(action))
            {
                _notificationService.Notify($"[To {user.Name}]: {message}");
            }
        }
    }

    public abstract void SubscribeToPublisherEvents(TPublisher publisher);
}
