namespace FootballRanking;

public class Ranking
{
    private readonly List<Team> teams;
    private readonly List<Match> matches;

    public Ranking(List<Team> teams, List<Match> matches)
    {
        this.teams = teams;
        this.matches = matches;
    }

    public void AddTeam(Team team)
    {
        teams.Add(team);
        teams.Sort((x, y) => x.GetPoints().CompareTo(y.GetPoints()));
    }

    public void AddMatch(Match match)
    {
        match.UpdatePointsByMatch();
        matches.Add(match);
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
