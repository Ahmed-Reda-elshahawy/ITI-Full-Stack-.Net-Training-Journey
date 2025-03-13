namespace observer.Models;

record Product
{
    private static int _identitySequence = 0;
    public int Id { get; } = ++_identitySequence;
    public string Name { get; set; }
    public double Price { get; set; }
}
