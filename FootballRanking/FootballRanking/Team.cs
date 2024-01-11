namespace FootballRanking;

public class Team
{
    private readonly int id;
    private readonly string name;
    private int points;

    public Team(int id, string name)
    {
        this.id = id;
        this.name = name;
        points = 0;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

    public string GetName()
    {
        return name;
    }

    public int GetPoints()
    {
        return points;
    }
}
