namespace FootballRanking;

public class Team
{
    readonly private int _id;
    readonly private string _coach;
    readonly private List<Player> _players;

    public Team(int id, string coach, List<Player> players)
    {
        this._id = id;
        this._coach = coach;
        this._players = players;
    }

    public int Id => this._id;

    public string Coach => this._coach;

    public List<Player> Players => this._players;
}