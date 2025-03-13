namespace ECOrdersRefactor;

public abstract class Order
{
    public int Id { get; set; }
    public double Price { get; set; }
    public abstract double CalculateTotalPrice();
}