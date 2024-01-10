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
        teams.Add(team);
        teams.Sort((x, y) => x.GetPoints().CompareTo(y.GetPoints()));
    }

    public Team GetTeamAtPosition(int position)
    {
        teams.Sort((x, y) => x.GetPoints().CompareTo(y.GetPoints()));
        return teams[position - 1];
    }

    public int GetPositionOfTeam(string name)
    {
        teams.Sort((x, y) => x.GetPoints().CompareTo(y.GetPoints()));
        return teams.FindIndex(t => t.GetName() == name) + 1;
    }
}
