namespace FootballRanking;

public enum DominantFoot
{
    Right,
    Left
}

public class Player
{
    private readonly int _id;
    private readonly string _name;
    private readonly string _nationality;
    private readonly int _age;
    private readonly double _height;
    private readonly DominantFoot _dominantFoot;

    public Player(int id, string name, string nationality, int age, double height, DominantFoot dominantFoot)
    {
        this._id = id;
        this._name = name;
        this._nationality = nationality;
        this._age = age;
        this._height = height;
        this._dominantFoot = dominantFoot;
    }

    public int Id => this._id;

    public string Name => this._name;

    public string Nationality => this._nationality;

    public int Age => this._age;

    public double Height => this._height;

    public DominantFoot DominantFoot => this._dominantFoot;
}