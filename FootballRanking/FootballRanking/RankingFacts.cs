using Xunit;

namespace FootballRanking.Tests;

public class RankingFacts
{
    [Fact]
    public void AddTeamTest()
    {
        Ranking ranking = new Ranking(new List<Team>(), new List<Match>());
        Team team = new Team("Team 1");
        team.AddPoints(3);
        ranking.AddTeam(team);

        Assert.Equal(team, ranking.GetTeamAtPosition(1));
    }

    [Fact]
    public void GetPositionOfTeamTest()
    {
        Ranking ranking = new Ranking(new List<Team>(), new List<Match>());
        Team teamA = new Team("team A");
        Team teamB = new Team("team B");
        ranking.AddTeam(teamA);
        ranking.AddTeam(teamB);

        int position = ranking.GetPositionOfTeam("team B");

        Assert.Equal(2, position);
    }

    [Fact]
    public void GetTeamAtPositionTest()
    {
        Ranking ranking = new Ranking(new List<Team>(), new List<Match>());

        Team teamA = new Team("team A");
        Team teamB = new Team("team B");
        ranking.AddTeam(teamA);
        ranking.AddTeam(teamB);

        int position = ranking.GetPositionOfTeam("team B");

        Assert.Equal(2, position);

        Team team = ranking.GetTeamAtPosition(2);

        Assert.Equal(teamB, team);
    }
}