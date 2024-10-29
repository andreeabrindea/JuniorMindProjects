namespace Linq;

public class ProductFromExercise10
{
    public ProductFromExercise10(string name, ICollection<Feature> features)
    {
        Name = name;
        Features = features;
    }

    public string Name { get; set; }

    public ICollection<Feature> Features { get; }
}