namespace FootballRanking;

public class Ranking
{
    private Team[] teams;

    public Ranking()
    {
        teams = new Team[] { };
    }

    public void Add(Team team)
    {
        int noOfTeams = teams.Length + 1;

        Array.Resize(ref teams,  noOfTeams);
        teams[noOfTeams - 1] = team;
        SortTeams();
    }

    public void Update(Match match)
    {
        match.UpdatePoints();
        SortTeams();
    }

    public Team GetTeamAtPosition(int position)
    {
        return teams[position - 1];
    }

    public int GetPositionOfTeam(Team givenTeam)
    {
        for (int i = 0; i < teams.Length; i++)
        {
            if (teams[i].Equals(givenTeam))
            {
                return i + 1;
            }
        }

        return -1;
    }

    private void SortTeams()
    {
        for (int i = 0; i < teams.Length - 1; i++)
        {
            for (int j = 0; j < teams.Length - i - 1; j++)
            {
                if (teams[j + 1].IsLessThan(teams[j]))
                {
                    (teams[i], teams[j + 1]) = (teams[j + 1], teams[i]);
                }
            }
        }
    }
}
