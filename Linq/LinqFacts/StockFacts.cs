using Xunit;

namespace Linq.Facts;

public class StockFacts
{
    [Fact]
    public void AddProduct_InvalidQuantity_ShouldThrowException()
    {
        Product chair = new Product("chair", 10.50);
        Stock stock = new Stock();
        Assert.Throws<ArgumentException>(() => stock.AddProduct(chair, -2));
    }

    [Fact]
    public void NotifyAboutStock_AfterSellingProductAndThresholdIsAttained()
    {
        Product chair = new Product("chair", 10.50);
        Product table = new Product("table", 12);
        Dictionary<Product, int> products = new Dictionary<Product, int>
        {
            { chair, 10 },
            { table, 23 },
        };

        Product lowStockProduct = null;
        int stockNumber = 0;

        Stock stock = new Stock();
        stock.Notify += (sender, e) =>
        {
            lowStockProduct = e.Product;
            stockNumber = e.Quantity;
        };
        stock.AddProducts(products);
        stock.SellProduct(chair);

        Assert.Equal(9, stockNumber);
        Assert.Equal(chair, lowStockProduct);
    }

    [Fact]
    public void NotifyAboutStock_AfterSellingSeveralProductsAndThresholdIsNotAttained()
    {
        Product chair = new Product("chair", 10.50);
        Product table = new Product("table", 12);
        Product tv = new Product("tv", 70);
        Product oven = new Product("oven", 20);
        Dictionary<Product, int> products = new Dictionary<Product, int>
        {
            { chair, 10 },
            { table, 23 },
            { tv, 12 },
            { oven, 10 },
        };

        Product lowStockProduct = null;
        int stockNumber = 0;
        Stock stock = new Stock();
        stock.Notify += (sender, e) =>
        {
            lowStockProduct = e.Product;
            stockNumber = e.Quantity;
        };
        stock.AddProducts(products);

        Dictionary<Product, int> productsToSell = new() { { chair, 2 }, { table, 1 }, { tv, 1 }, { oven, 3 } };
        stock.SellSeveralProducts(productsToSell);
        Assert.Equal(7, stockNumber);
        Assert.Equal(oven, lowStockProduct);
    }

    [Fact]
    public void NotifyAboutStock_WhenThresholdIsNotAttained()
    {
        Product chair = new Product("chair", 10.50);
        Product lowStockProduct = null;
        int stockNumber = 0;
        Stock stock = new Stock();
        stock.Notify += (sender, e) =>
        {
            lowStockProduct = e.Product;
            stockNumber = e.Quantity;
        };

        stock.AddProduct(chair, 9);
        stock.SellProductByQuantity(chair, 2);
        Assert.Equal(0, stockNumber);
        Assert.Null(lowStockProduct);
    }

    [Fact]
    public void NotifyAboutStock_WhenThresholdIsAttained()
    {
        Product chair = new Product("chair", 10.50);
        Dictionary<Product, int> products = new Dictionary<Product, int>
        {
            { chair, 6 },
        };

        Product lowStockProduct = null;
        int stockNumber = 0;

        Stock stock = new Stock();
        stock.Notify += (sender, e) =>
        {
            lowStockProduct = e.Product;
            stockNumber = e.Quantity;
        };

        stock.AddProducts(products);

        Dictionary<Product, int> productsToSell = new() { { chair, 2 } };
        stock.SellSeveralProducts(productsToSell);
        Assert.Equal(4, stockNumber);
        Assert.Equal(chair, lowStockProduct);
    }
}