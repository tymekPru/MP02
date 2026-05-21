namespace MP02
{
    public class Customer(int id, string fullName, string email)
    {
        private readonly List<Order> _orders = [];

        public int Id { get; } = id;
        public string FullName { get; } = fullName;
        public string Email { get; } = email;

        public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

        public void AddOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (_orders.Contains(order))
                return;

            order.Customer?.RemoveOrder(order);

            _orders.Add(order);

            order.SetCustomer(this);
        }

        public void RemoveOrder(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            if (_orders.Remove(order))
            {
                order.SetCustomer(null);
            }
        }

        public override string ToString()
        {
            return $"Customer: {FullName}, Email: {Email}, Orders: {_orders.Count}";
        }
    }
}