namespace FootballRanking;

public enum DominantFoot
{
    Right,
    Left
}

public class Player
{
    private readonly int id;
    private readonly string name;
    private readonly string nationality;
    private readonly int age;
    private readonly double height;
    private readonly DominantFoot dominantFoot;

    public Player(int id, string name, string nationality, int age, double height, DominantFoot dominantFoot)
    {
        this.id = id;
        this.name = name;
        this.nationality = nationality;
        this.age = age;
        this.height = height;
        this.dominantFoot = dominantFoot;
    }
}