namespace Linq;

public class Stock
{
    public Stock()
    {
        this.ProductsStock = new Dictionary<Product, int>();
    }

    public Action<Product, int> Notify { get; set; }

    private Dictionary<Product, int> ProductsStock { get; }

    public void AddProduct(Product product, int quantity)
    {
        if (quantity < 1)
        {
            throw new ArgumentException("Quantity should be a positive non-zero integer.");
        }

        if (ProductsStock.ContainsKey(product))
        {
            ProductsStock[product] += quantity;
            if (IsThresholdAttained(product))
            {
                NotifyAboutStock(product);
            }

            return;
        }

        ProductsStock.Add(product, quantity);
        if (!IsThresholdAttained(product))
        {
            return;
        }

        NotifyAboutStock(product);
    }

    public void AddProducts(Dictionary<Product, int> products)
    {
        foreach (var product in products)
        {
            AddProduct(product.Key, product.Value);
        }
    }

    public void RemoveProduct(Product product) => ProductsStock.Remove(product);

    public void SellProduct(Product product)
    {
        const string notInStockMessage = "Product is not in stock";
        if (!ProductsStock.ContainsKey(product))
        {
            throw new InvalidOperationException(notInStockMessage);
        }

        var quantity = ProductsStock[product];
        if (quantity > 0)
        {
            ProductsStock[product]--;
            if (IsThresholdAttained(product))
            {
                NotifyAboutStock(product);
            }
        }
        else
        {
            throw new ArgumentException(notInStockMessage);
        }
    }

    public void SellProductByQuantity(Product product, int productQuantity)
    {
        if (!ProductsStock.ContainsKey(product))
        {
            throw new InvalidOperationException("Product is not in stock");
        }

        var quantity = ProductsStock[product];
        if (quantity > 0)
        {
            ProductsStock[product] -= productQuantity;
            if (IsThresholdAttained(product))
            {
                NotifyAboutStock(product);
            }
        }
        else
        {
            throw new ArgumentException("Product is not in stock");
        }
    }

    public void SellSeveralProducts(List<Product> products)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        products.ForEach(SellProduct);
    }

    public void SellSeveralProducts(Dictionary<Product, int> products)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        products.ToList().ForEach(product => SellProductByQuantity(product.Key, product.Value));
    }

    private void NotifyAboutStock(Product stockProduct) => Notify.Invoke(stockProduct, ProductsStock[stockProduct]);

    private bool IsThresholdAttained(Product product)
    {
        const int first = 1;
        const int second = 4;
        const int third = 9;
        int[] thresholds = { first, second, third };
        return thresholds.Contains(ProductsStock[product]);
    }
}