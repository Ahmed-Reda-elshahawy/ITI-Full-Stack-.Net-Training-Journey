namespace observer.Services.Notifications;

interface INotificationStrategy
{
    void Notify(string message);
}
