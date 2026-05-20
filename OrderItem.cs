namespace MP02
{
    public class OrderItem(Order order, Product product, int quantity, decimal unitPrice)
    {
        public int Quantity { get; } = quantity;
        public decimal UnitPrice { get; } = unitPrice;

        public decimal TotalPrice => Quantity * UnitPrice;

        public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));
        public Product Product { get; } = product ?? throw new ArgumentNullException(nameof(product));

        public override string ToString()
        {
            return $"OrderItem: {Product.Name}, Quantity: {Quantity}, UnitPrice: {UnitPrice} PLN, Total: {TotalPrice} PLN";
        }
    }
}