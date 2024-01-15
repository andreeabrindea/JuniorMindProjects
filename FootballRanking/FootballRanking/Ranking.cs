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
        SortTeamsByPoints();
    }

    public Team GetTeamAtPosition(int position)
    {
        SortTeamsByPoints();
        return teams[position - 1];
    }

    public int GetPositionOfTeam(Team givenTeam)
    {
        SortTeamsByPoints();
        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i].Equals(givenTeam))
            {
                return i + 1;
            }
        }

        return -1;
    }

    private void SortTeamsByPoints()
    {
        for (int i = 0; i < teams.Length - 1; i++)
        {
            for (int j = 0; j < teams.Length - i - 1; j++)
            {
                if (teams[i].IsLessThan(teams[j + 1]))
                {
                    (teams[i], teams[j + 1]) = (teams[j + 1], teams[i]);
                }
            }
        }
    }
}
