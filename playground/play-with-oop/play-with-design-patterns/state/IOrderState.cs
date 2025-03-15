namespace state;

interface IOrderState
{
    void Process(Order order);
    void Ship(Order order);
    void Cancel(Order order);
    string GetStatus();
}
