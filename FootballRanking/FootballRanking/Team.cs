namespace FootballRanking;

public class Team
{
    private readonly int id;
    private readonly string name;
    private readonly string coach;
    private readonly List<Player> players;
    private int points;

    public Team(int id, string name, string coach, List<Player> players)
    {
        this.id = id;
        this.name = name;
        this.coach = coach;
        this.players = players;
        this.points = 0;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public string GetName()
    {
        return this.name;
    }

    public int GetPoints()
    {
        return this.points;
    }
}
