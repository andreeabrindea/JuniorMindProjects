using Xunit;

namespace FootballRanking;

public class MatchFacts
{
    public void UpdatePointsByMatchAfterAWonMatchAndADrawMatch()
    {
        Team teamA = new Team(1, "teamA");
        Team teamB = new Team(2, "teamB");

        Match match1 = new Match(1, teamA, teamB, 2, 1);
        match1.UpdatePointsByMatch();

        Assert.Equal(3, teamA.GetPoints());
        Assert.Equal(0, teamB.GetPoints());

        Match match2 = new Match(1, teamA, teamB, 2, 2);
        match2.UpdatePointsByMatch();

        Assert.Equal(4, teamA.GetPoints());
        Assert.Equal(1, teamB.GetPoints());
    }
}