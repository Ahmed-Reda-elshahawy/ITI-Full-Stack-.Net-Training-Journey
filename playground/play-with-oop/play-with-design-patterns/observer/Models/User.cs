namespace observer.Models;

record User
{
    private static int _identitySequence = 0;
    public int Id { get; } = ++_identitySequence;
    public string Name { get; set; }
    public List<SubscriptionAction> SubscribedActions { get; set; } = new();
}
