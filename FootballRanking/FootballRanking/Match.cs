namespace FootballRanking;

public class Match
{
    private readonly Team homeTeam;
    private readonly Team awayTeam;
    private readonly int homeTeamGoals;
    private readonly int awayTeamGoals;

    public Match(Team homeTeam, Team awayTeam, int homeTeamGoals, int awayTeamGoals)
    {
        this.homeTeam = homeTeam;
        this.awayTeam = awayTeam;
        this.homeTeamGoals = homeTeamGoals;
        this.awayTeamGoals = awayTeamGoals;
    }

    public void UpdatePoints()
    {
        if (homeTeamGoals > awayTeamGoals)
        {
            homeTeam.AddPoints(3);
        }
        else if (awayTeamGoals > homeTeamGoals)
        {
            awayTeam.AddPoints(3);
        }
        else
        {
            homeTeam.AddPoints(1);
            awayTeam.AddPoints(1);
        }
    }
}