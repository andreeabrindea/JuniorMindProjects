namespace FootballRanking;

public class League
{
    private readonly int _id;
    private readonly string _name;
    private readonly string _country;
    private readonly Season _season;

    public League(int id, string name, string country, Season season)
    {
        this._id = id;
        this._name = name;
        this._country = country;
        this._season = season;
    }

    public int Id => this._id;

    public string Name => this._name;

    public string Country => this._country;

    public Season Season => this._season;
}