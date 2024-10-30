using Xunit;

namespace Linq.Facts;

public class StockFacts
{
    [Fact]
    public void AddProduct_InvalidQuantity_ShouldThrowException()
    {
        Product chair = new Product("chair", 10.50);
        Action<Product, int> notification = null;
        Stock stock = new Stock(notification);
        Assert.Throws<ArgumentException>(() => stock.AddProduct(chair, -2));
    }

    [Fact]
    public void AddProduct_AddexistingProduct_ShouldIncreaseQuantity()
    {
        Product chair = new Product("chair", 10.50);
        Product table = new Product("table", 12);
        Dictionary<Product, int> products = new Dictionary<Product, int>
        {
            { chair, 10 },
            { table, 23 },
        };

        Action<Product, int> notification = null;
        Stock stock = new Stock(products, notification);
        stock.AddProduct(chair, 2);
        Assert.Equal(12, stock.ProductsStock[chair]);
    }

    [Fact]
    public void NotifyAboutStock_AfterSellingProduct()
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
        Action<Product, int> notification = (product, remainingStock) =>
        {
            lowStockProduct = product;
            stockNumber = remainingStock;
        };

        Stock stock = new Stock(products, notification);
        stock.SellProduct(chair);

        Assert.Equal(9, stockNumber);
        Assert.Equal(chair, lowStockProduct);
    }

    [Fact]
    public void SellSeveralProducts()
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
        Action<Product, int> notification = (product, remainingStock) =>
        {
            lowStockProduct = product;
            stockNumber = remainingStock;
        };

        Stock stock = new Stock(products, notification);
        List<Product> productsToSell = new() { chair, table, tv, oven, chair, oven, oven };
        stock.SellSeveralProducts(productsToSell);
        Assert.Equal(
            new Dictionary<Product, int>
        {
            { chair, 8 },
            { table, 22 },
            { tv, 11 },
            { oven, 7 },
        }, products);

        Assert.Equal(9, stockNumber);
        Assert.Equal(oven, lowStockProduct);
    }

    [Fact]
    public void SellSeveralProductsWithQuantity()
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
        Action<Product, int> notification = (product, remainingStock) =>
        {
            lowStockProduct = product;
            stockNumber = remainingStock;
        };

        Stock stock = new Stock(products, notification);
        Dictionary<Product, int> productsToSell = new() { { chair, 2 }, { table, 1 }, { tv, 1 }, { oven, 3 } };
        stock.SellSeveralProducts(productsToSell);
        Assert.Equal(
            new Dictionary<Product, int>
            {
                { chair, 8 },
                { table, 22 },
                { tv, 11 },
                { oven, 7 },
            }, products);

        Assert.Equal(0, stockNumber);
        Assert.Equal(null, lowStockProduct);
    }
}