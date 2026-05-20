namespace MP02
{
    public class Warehouse(int id, string name)
    {
        private readonly Dictionary<string, Product> _productsBySku = [];

        public int Id { get; } = id;
        public string Name { get; } = name;

        public IReadOnlyDictionary<string, Product> ProductsBySku => _productsBySku;

        public void AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            if (_productsBySku.ContainsKey(product.SKU))
                throw new InvalidOperationException($"Product with SKU {product.SKU} already exists in this warehouse.");

            product.Warehouse?.RemoveProduct(product.SKU);

            _productsBySku.Add(product.SKU, product);

            product.SetWarehouse(this);
        }

        public void RemoveProduct(string sku)
        {
            if (_productsBySku.TryGetValue(sku, out var product))
            {
                _productsBySku.Remove(sku);

                product.SetWarehouse(null);
            }
        }

        public Product? FindProductBySku(string sku)
        {
            _productsBySku.TryGetValue(sku, out var product);
            return product;
        }

        public override string ToString()
        {
            return $"Warehouse: {Name}, Products: {_productsBySku.Count}";
        }
    }
}