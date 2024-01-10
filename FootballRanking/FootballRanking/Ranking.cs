namespace FootballRanking;

public class Ranking
{
    private readonly List<Team> teams;

    public Ranking(List<Team> teams)
    {
        this.teams = teams;
    }

    public void AddTeam(Team team)
    {
        this.teams.Add(team);
    }

    public Team GetTeamAtPosition(int position)
    {
        return this.teams[position - 1];
    }

    public int GetPositionOfTeam(string name)
    {
        return this.teams.FindIndex(t => t.GetName() == name) + 1;
    }

    public void UpdateRanking(string winningTeamName)
    {
        Team winningTeam = this.teams.Find(t => t.GetName() == winningTeamName);
        winningTeam.AddPoints(3);
    }
}
