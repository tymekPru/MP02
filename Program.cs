using MP02;

Console.WriteLine("=== MAS Project - Online Store Associations ===");
Console.WriteLine();

var customer = new Customer(
    id: 1,
    fullName: "Jan Kowalski",
    email: "jan.kowalski@example.com"
);

var warehouse = new Warehouse(
    id: 1,
    name: "Main Warehouse"
);

var laptop = new Product(
    sku: "LAPTOP-001",
    name: "Laptop Lenovo",
    basePrice: 3500m
);

var mouse = new Product(
    sku: "MOUSE-001",
    name: "Logitech Mouse",
    basePrice: 120m
);

var keyboard = new Product(
    sku: "KEYBOARD-001",
    name: "Mechanical Keyboard",
    basePrice: 300m
);

warehouse.AddProduct(laptop);
warehouse.AddProduct(mouse);
warehouse.AddProduct(keyboard);

Console.WriteLine("1. Qualified association: Warehouse - Product [SKU]");
Console.WriteLine(warehouse);

var foundProduct = warehouse.FindProductBySku("MOUSE-001");

Console.WriteLine($"Found by SKU: {foundProduct?.Name}");
Console.WriteLine($"Reverse link Product -> Warehouse: {foundProduct?.Warehouse?.Name}");
Console.WriteLine();

var order = new Order(
    number: "ORD/2026/001",
    createdAt: DateTime.Today
);

customer.AddOrder(order);

Console.WriteLine("2. Regular association: Customer 1 - 0..* Order");
Console.WriteLine(customer);

foreach (var customerOrder in customer.Orders)
{
    Console.WriteLine($"Order: {customerOrder.Number}, Customer from reverse link: {customerOrder.Customer?.FullName}");
}

Console.WriteLine();

order.AddProduct(laptop, quantity: 1, unitPrice: laptop.BasePrice);
order.AddProduct(mouse, quantity: 2, unitPrice: mouse.BasePrice);
order.AddProduct(keyboard, quantity: 1, unitPrice: keyboard.BasePrice);

Console.WriteLine("3. Association with attribute: Order 0..* - 0..* Product through OrderItem");
Console.WriteLine(order);

foreach (var item in order.Items)
{
    Console.WriteLine(item);
}

Console.WriteLine();

Console.WriteLine($"Reverse links from Product '{mouse.Name}' to OrderItems:");

foreach (var item in mouse.OrderItems)
{
    Console.WriteLine($"Order: {item.Order.Number}, Quantity: {item.Quantity}");
}

Console.WriteLine();

order.AddPayment(
    amount: 2000m,
    method: "BLIK",
    paidAt: DateTime.Today
);

order.AddPayment(
    amount: order.TotalValue - 2000m,
    method: "Card",
    paidAt: DateTime.Today
);

Console.WriteLine("4. Composition: Order 1 - 0..* Payment");
Console.WriteLine($"Order: {order.Number}");

foreach (var payment in order.Payments)
{
    Console.WriteLine($"{payment}, Reverse link Payment -> Order: {payment.Order.Number}");
}

Console.WriteLine();

Console.WriteLine($"Total order value: {order.TotalValue} PLN");
Console.WriteLine($"Total paid: {order.TotalPaid} PLN");