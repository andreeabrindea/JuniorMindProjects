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
            return;
        }

        ProductsStock.Add(product, quantity);
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
            if (IsThresholdAttained(product, quantity))
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
            if (IsThresholdAttained(product, productQuantity))
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
        foreach (var product in products)
        {
            SellProductByQuantity(product.Key, product.Value);
        }
    }

    private void NotifyAboutStock(Product stockProduct) => Notify.Invoke(stockProduct, ProductsStock[stockProduct]);

    private bool IsThresholdAttained(Product product, int quantity)
    {
        int closestThreshold = FindTheClosestThreshold(ProductsStock[product], quantity);
        return closestThreshold != 0 && closestThreshold > ProductsStock[product];
    }

    private int FindTheClosestThreshold(int quantity, int soldQuantity)
    {
        int[] thresholds = { 10, 5, 2 };
        return thresholds.FirstOrDefault(threshold => threshold <= quantity + soldQuantity);
    }
}