using state;

Order order = new Order(150.0);

Console.WriteLine($"Order Status: {order.Status}");
order.Process();  // Order is now Processed.

Console.WriteLine($"Order Status: {order.Status}");
order.Cancel();   // Order has been Canceled.

Console.WriteLine($"Order Status: {order.Status}");
order.Ship();     // Canceled orders cannot be Shipped.