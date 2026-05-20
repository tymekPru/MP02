namespace MP02
{
    public class Order(string number, DateTime createdAt)
    {
        private readonly List<OrderItem> _items = new();
        private readonly List<Payment> _payments = new();

        public string Number { get; } = number;
        public DateTime CreatedAt { get; } = createdAt;

        public Customer? Customer { get; private set; }

        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
        public IReadOnlyList<Payment> Payments => _payments.AsReadOnly();

        public decimal TotalValue => _items.Sum(item => item.TotalPrice);
        public decimal TotalPaid => _payments.Sum(payment => payment.Amount);

        internal void SetCustomer(Customer? customer)
        {
            Customer = customer;
        }

        public void AddProduct(Product product, int quantity, decimal unitPrice)
        {
            ArgumentNullException.ThrowIfNull(product);

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0.");

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative.");

            var item = new OrderItem(this, product, quantity, unitPrice);

            _items.Add(item);

            product.AddOrderItem(item);
        }

        public void RemoveProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            var item = _items.FirstOrDefault(i => i.Product == product);

            if (item == null)
                return;

            _items.Remove(item);

            product.RemoveOrderItem(item);
        }

        public void AddPayment(decimal amount, string method, DateTime paidAt)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than 0.");

            if (string.IsNullOrWhiteSpace(method))
                throw new ArgumentException("Method cannot be empty.");

            var payment = new Payment(this, amount, method, paidAt);

            _payments.Add(payment);
        }

        public void RemovePayment(Payment payment)
        {
            ArgumentNullException.ThrowIfNull(payment);

            _payments.Remove(payment);
        }

        public override string ToString()
        {
            return $"Order: {Number}, CreatedAt: {CreatedAt:yyyy-MM-dd}, Items: {_items.Count}, Payments: {_payments.Count}";
        }
    }
}