namespace FootballRanking;

public class Season
{
    private readonly int _id;
    private readonly string _timeFrame;
    private List<Team> _teams;
    private List<Match> _matches;
    private TeamPoints ranking;

    public Season(int id, string timeFrame, List<Team> teams, List<Match> matches, TeamPoints ranking)
    {
        _id = id;
        _timeFrame = timeFrame;
        _teams = teams;
        _matches = matches;
        this.ranking = ranking;
    }

    public int Id => _id;

    public string TimeFrame => _timeFrame;

    public List<Team> Teams
    {
        get => _teams;
        set => _teams = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Match> Matches
    {
        get => _matches;
        set => _matches = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TeamPoints Ranking
    {
        get => ranking;
        set => ranking = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TeamPoints GetPoints(Team team)
    {
        int points = 0;

        foreach (var match in this.Matches)
        {
            if (match.HomeTeam.Id == team.Id)
            {
                if (match.HomeTeamGoals > match.AwayTeamGoals)
                {
                    points += 3;
                }

                if (match.HomeTeamGoals == match.AwayTeamGoals)
                {
                    points += 1;
                }
            }

            if (match.AwayTeam.Id == team.Id)
            {
                if (match.AwayTeamGoals > match.HomeTeamGoals)
                {
                    points += 3;
                }

                if (match.AwayTeamGoals == match.HomeTeamGoals)
                {
                    points += 1;
                }
            }
        }

        return new TeamPoints(team, points);
    }
}