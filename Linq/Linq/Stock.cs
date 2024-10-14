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

    public Action<Product, int> Notify { get; }

    public void AddProduct(Product product, int quantity)
    {
        if (quantity < 1)
        {
            throw new ArgumentException("Quantity should be a positive non-zero integer.");
        }

        if (ProductsStock.ContainsKey(product))
        {
            ProductsStock[product] += quantity;
            NotifyAboutStock();
            return;
        }

        ProductsStock.Add(product, quantity);
        NotifyAboutStock();
    }

    public void RemoveProduct(Product product) => ProductsStock.Remove(product);

    public void SellProduct(Product product)
    {
        if (!ProductsStock.ContainsKey(product))
        {
            throw new InvalidOperationException("Product is not in stock");
        }

        var quantity = ProductsStock[product];
        if (quantity > 0)
        {
            ProductsStock[product]--;
            NotifyAboutStock();
        }
        else
        {
            throw new ArgumentException("Product is not in stock");
        }
    }

    private void NotifyAboutStock()
    {
        const int ten = 10;
        ProductsStock
            .Where(product => product.Value < ten)
            .ToList()
            .ForEach(product => Notify.Invoke(product.Key, product.Value));
    }
}