namespace FootballRanking;

public class TeamPoints
{
    private Team _team;
    private int _points;

    public TeamPoints(Team team, int points)
    {
        _team = team;
        _points = points;
    }

    public Team Team
    {
        get => _team;
        set => _team = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Points
    {
        get => _points;
        set => _points = value;
    }
}