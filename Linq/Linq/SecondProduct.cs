namespace Linq;

public class SecondProduct
{
    public SecondProduct(string name, ICollection<Feature> features)
    {
        Name = name;
        Features = features;
    }

    public string Name { get; set; }

    public ICollection<Feature> Features { get; }
}