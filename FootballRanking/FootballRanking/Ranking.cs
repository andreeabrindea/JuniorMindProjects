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
        int n = teams.Length;
        while (n > 0)
        {
            int lastModifiedIndex = 0;
            for (int currentIndex = 1; currentIndex < n; currentIndex++)
            {
                if (teams[currentIndex - 1].IsLessThan(teams[currentIndex]))
                {
                    (teams[currentIndex - 1], teams[currentIndex]) = (teams[currentIndex], teams[currentIndex - 1]);
                    lastModifiedIndex = currentIndex;
                }
            }

            n = lastModifiedIndex;
        }
    }
}
