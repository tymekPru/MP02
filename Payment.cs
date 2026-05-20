namespace MP02
{
    public class Payment(Order order, decimal amount, string method, DateTime paidAt)
    {
        public decimal Amount { get; } = amount;
        public string Method { get; } = method;
        public DateTime PaidAt { get; } = paidAt;

        public Order Order { get; } = order ?? throw new ArgumentNullException(nameof(order));

        public override string ToString()
        {
            return $"Payment: {Amount} PLN, Method: {Method}, PaidAt: {PaidAt:yyyy-MM-dd}";
        }
    }
}