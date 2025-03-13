namespace observer.Models;

record Offer
{
    private static int _identitySequence = 0;
    public int Id { get; } = ++_identitySequence;
    public int ProductId { get; set; }
    public double Discount { get; set; }
}
