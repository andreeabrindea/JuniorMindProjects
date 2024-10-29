namespace Linq;

public class Stock
{
    public Stock(Dictionary<Product, int> stock, Action<Product, int> notify)
    {
        this.ProductsStock = stock;
        this.Notify = notify;
    }

    public Stock(Action<Product, int> notify)
    {
        this.ProductsStock = new Dictionary<Product, int>();
        this.Notify = notify;
    }

    public Dictionary<Product, int> ProductsStock { get; }

    public Action<Product, int> Notify { get; set; }

    public void AddProduct(Product product, int quantity)
    {
        if (quantity < 1)
        {
            throw new ArgumentException("Quantity should be a positive non-zero integer.");
        }

        if (ProductsStock.ContainsKey(product))
        {
            ProductsStock[product] += quantity;
            NotifyAboutStock(product);
            return;
        }

        ProductsStock.Add(product, quantity);
        NotifyAboutStock(product);
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
            NotifyAboutStock(product);
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
            NotifyAboutStock(product);
        }
        else
        {
            throw new ArgumentException("Product is not in stock");
        }
    }

    public void SellSeveralProducts(List<Product> products)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        foreach (var product in products)
        {
            SellProduct(product);
        }
    }

    public void SellSeveralProducts(Dictionary<Product, int> products)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(products));
        foreach (var product in products)
        {
            SellProductByQuantity(product.Key, product.Value);
        }
    }

    private void NotifyAboutStock(Product stockProduct)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(stockProduct));
        const int ten = 10;
        if (ProductsStock[stockProduct] >= ten)
        {
            return;
        }

        Notify.Invoke(stockProduct, ProductsStock[stockProduct]);
    }
}