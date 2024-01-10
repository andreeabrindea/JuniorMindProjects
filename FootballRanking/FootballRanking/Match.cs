namespace FootballRanking;

public class Match
{
    private readonly int id;
    private readonly Team homeTeam;
    private readonly Team awayTeam;
    private readonly int homeTeamGoals;
    private readonly int awayTeamGoals;

    public Match(int id, Team homeTeam, Team awayTeam, int homeTeamGoals, int awayTeamGoals)
    {
        this.id = id;
        this.homeTeam = homeTeam;
        this.awayTeam = awayTeam;
        this.homeTeamGoals = homeTeamGoals;
        this.awayTeamGoals = awayTeamGoals;
    }

    public void UpdatePointsByMatch()
    {
        if (homeTeamGoals > awayTeamGoals)
        {
            homeTeam.AddPoints(3);
        }

        if (awayTeamGoals > homeTeamGoals)
        {
            awayTeam.AddPoints(3);
        }

        homeTeam.AddPoints(1);
        awayTeam.AddPoints(1);
    }
}