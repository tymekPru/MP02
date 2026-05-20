namespace MP02
{
    public class Product(string sku, string name, decimal basePrice)
    {
        private readonly List<OrderItem> _orderItems = [];

        public string SKU { get; } = sku;
        public string Name { get; } = name;
        public decimal BasePrice { get; } = basePrice;

        public Warehouse? Warehouse { get; private set; }

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        internal void SetWarehouse(Warehouse? warehouse)
        {
            Warehouse = warehouse;
        }

        internal void AddOrderItem(OrderItem item)
        {
            if (!_orderItems.Contains(item))
            {
                _orderItems.Add(item);
            }
        }

        internal void RemoveOrderItem(OrderItem item)
        {
            _orderItems.Remove(item);
        }

        public override string ToString()
        {
            return $"Product: {Name}, SKU: {SKU}, BasePrice: {BasePrice} PLN";
        }
    }
}