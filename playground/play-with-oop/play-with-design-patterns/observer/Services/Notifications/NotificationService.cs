namespace observer.Services.Notifications;

class NotificationService(INotificationStrategy _notificationStrategy)
{
    public void Notify(string message)
    {
        _notificationStrategy.Notify(message);
    }
}