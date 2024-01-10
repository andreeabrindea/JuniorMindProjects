using Xunit;

namespace FootballRanking;

public class MatchFacts
{
    public void UpdatePointsByMatchAfterAWonMatchAndADrawMatch()
    {
        Player player1 = new Player(1, "John", "British", 30, 1.80, DominantFoot.Right);
        Player player2 = new Player(2, "Isak", "Swedish", 26, 1.82, DominantFoot.Left);

        List<Player> playersA = new List<Player>();
        playersA.Add(player1);
        playersA.Add(player2);

        Team teamA = new Team(1, "teamA", "William", playersA);

        Player player3 = new Player(3, "Mihai", "Romanian", 30, 1.80, DominantFoot.Right);
        Player player4 = new Player(4, "William", "Irish", 26, 1.82, DominantFoot.Left);

        List<Player> playersB = new List<Player>();
        playersB.Add(player3);
        playersB.Add(player4);

        Team teamB = new Team(2, "teamB", "Edgar", playersB);

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