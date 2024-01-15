namespace FootballRanking;

public class Ranking
{
    private Team[] teams;

    public Ranking()
    {
        teams = new Team[] { };
    }

    public void AddTeam(Team team)
    {
        int noOfTeams = teams.Length + 1;

        Array.Resize(ref teams,  noOfTeams);
        teams[noOfTeams - 1] = team;
    }

    public void UpdatePointsAfterMatch(Match match)
    {
        match.UpdatePointsByMatch();
        teams = SortTeamsByPoints(teams);
    }

    public Team GetTeamAtPosition(int position)
    {
        teams = SortTeamsByPoints(teams);
        return teams[position - 1];
    }

    public int GetPositionOfTeam(Team givenTeam)
    {
        teams = SortTeamsByPoints(teams);
        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i].Equals(givenTeam))
            {
                return i + 1;
            }
        }

        return -1;
    }

    public Team[] SortTeamsByPoints(Team[] teams)
    {
        for (int i = 0; i < teams.Length - 1; i++)
        {
            for (int j = i + 1; j < teams.Length; j++)
            {
                if (teams[i].IsLessThan(teams[j]))
                {
                    (teams[i], teams[j]) = (teams[j], teams[i]);
                }
            }
        }

        return teams;
    }
}
