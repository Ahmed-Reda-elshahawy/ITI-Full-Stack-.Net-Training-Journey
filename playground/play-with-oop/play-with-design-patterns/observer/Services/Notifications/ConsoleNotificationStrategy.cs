namespace observer.Services.Notifications;

class ConsoleNotificationStrategy : INotificationStrategy
{
    public void Notify(string message)
    {
        var defaultForeColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Notification: {message}");
        Console.ForegroundColor = defaultForeColor;
    }
}
