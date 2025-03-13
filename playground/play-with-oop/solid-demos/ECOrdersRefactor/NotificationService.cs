
namespace ECOrdersRefactor;

class NotificationService
{
    public void SendEmailNotification(Customer customer, string message)
    {
        Console.WriteLine($"Sending email notification to: {customer.Name} with message {message}.");
    }
}
