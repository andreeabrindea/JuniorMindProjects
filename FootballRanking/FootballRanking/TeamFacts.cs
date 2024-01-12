using Xunit;

namespace FootballRanking;

public class TeamFacts
{
    [Fact]
    public void AddPointsTest()
    {
        Team testTeam = new Team("test team");
        testTeam.AddPoints(3);
        Assert.Equal(3, testTeam.GetPoints());
    }
}