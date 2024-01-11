namespace FootballRanking;

public class Team
{
    private readonly string name;
    private int points;

    public Team(string name)
    {
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
