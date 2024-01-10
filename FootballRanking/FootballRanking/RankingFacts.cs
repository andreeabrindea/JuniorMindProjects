using Xunit;

namespace FootballRanking.Tests;

public class RankingFacts
{
    [Fact]
    public void AddTeamTest()
    {
        Ranking ranking = new Ranking(new List<Team>());

        Player player1 = new Player(1, "player 1", "Romanian", 24, 1.82, DominantFoot.Right);
        Player player2 = new Player(2, "player 2", "Romanian", 25, 1.85, DominantFoot.Left);

        List<Player> players = new List<Player>();
        players.Add(player1);
        players.Add(player2);

        Team team = new Team(1, "Team 1", "Coach 1", players);
        team.AddPoints(3);
        ranking.AddTeam(team);

        Assert.Equal(team, ranking.GetTeamAtPosition(1));
    }

    [Fact]
    public void GetPositionOfTeamTest()
    {
        Ranking ranking = new Ranking(new List<Team>());

        Player playerA = new Player(1, "playerA", "Romanian", 24, 1.82, DominantFoot.Right);
        Player playerB = new Player(2, "playerB", "Romanian", 25, 1.85, DominantFoot.Left);

        List<Player> playersA = new List<Player>();
        playersA.Add(playerA);

        List<Player> playersB = new List<Player>();
        playersB.Add(playerB);

        Team teamA = new Team(1, "team A", "John", playersA);
        Team teamB = new Team(2, "team B", "William", playersB);
        ranking.AddTeam(teamA);
        ranking.AddTeam(teamB);

        int position = ranking.GetPositionOfTeam("team B");

        Assert.Equal(2, position);
    }

    [Fact]
    public void UpdateRankingTest()
    {
        Ranking ranking = new Ranking(new List<Team>());

        Player playerA = new Player(1, "playerA", "Romanian", 24, 1.82, DominantFoot.Right);
        Player playerB = new Player(2, "playerB", "Romanian", 25, 1.85, DominantFoot.Left);

        List<Player> playersA = new List<Player>();
        playersA.Add(playerA);

        List<Player> playersB = new List<Player>();
        playersB.Add(playerB);

        Team teamA = new Team(1, "team A", "John", playersA);
        Team teamB = new Team(2, "team B", "William", playersB);
        ranking.AddTeam(teamA);
        ranking.AddTeam(teamB);

        ranking.UpdateRanking("team A");

        Assert.Equal(3, teamA.GetPoints());
        Assert.Equal(0, teamB.GetPoints());
    }

    [Fact]
    public void GetTeamAtPositionTest()
    {
        Ranking ranking = new Ranking(new List<Team>());

        Player playerA = new Player(1, "playerA", "Romanian", 24, 1.82, DominantFoot.Right);
        Player playerB = new Player(2, "playerB", "Romanian", 25, 1.85, DominantFoot.Left);

        List<Player> playersA = new List<Player>();
        playersA.Add(playerA);

        List<Player> playersB = new List<Player>();
        playersB.Add(playerB);

        Team teamA = new Team(1, "team A", "John", playersA);
        Team teamB = new Team(2, "team B", "William", playersB);
        ranking.AddTeam(teamA);
        ranking.AddTeam(teamB);

        int position = ranking.GetPositionOfTeam("team B");

        Assert.Equal(2, position);

        Team team = ranking.GetTeamAtPosition(2);

        Assert.Equal(teamB, team);
    }
}