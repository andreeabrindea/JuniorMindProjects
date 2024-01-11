using Xunit;

namespace FootballRanking;

public class MatchFacts
{
    public void UpdatePointsByMatchAfterAWonMatchAndADrawMatch()
    {
        Team teamA = new Team("teamA");
        Team teamB = new Team("teamB");

        Match match1 = new Match(teamA, teamB, 2, 1);
        match1.UpdatePointsByMatch();

        Assert.Equal(3, teamA.GetPoints());
        Assert.Equal(0, teamB.GetPoints());

        Match match2 = new Match(teamA, teamB, 2, 2);
        match2.UpdatePointsByMatch();

        Assert.Equal(4, teamA.GetPoints());
        Assert.Equal(1, teamB.GetPoints());
    }
}