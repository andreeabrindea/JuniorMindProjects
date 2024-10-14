namespace Linq;

public class Product
{
    public Product(string name, double price)
    {
        this.Name = name;
        this.Price = price;
    }

    public string Name { get; }

    public double Price { get; }
}