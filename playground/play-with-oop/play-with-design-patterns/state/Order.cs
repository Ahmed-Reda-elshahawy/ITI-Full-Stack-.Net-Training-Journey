namespace state;

record Order
{
    private IOrderState _orderState;
    public string Id { get; } = Guid.NewGuid().ToString();
    public double Price { get; set; }
    public string Status => _orderState.GetStatus();
    public IOrderState OrderState { set => _orderState = value; }


    public Order(double price)
    {
        Price = price;
        OrderState = new NewOrderState();
    }

    public void Process() => _orderState.Process(this);
    public void Ship() => _orderState.Ship(this);
    public void Cancel() => _orderState.Cancel(this);
}
