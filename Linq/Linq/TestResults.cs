namespace Linq;

public class TestResults
{
    public TestResults(string id, string familyId, int score)
    {
        Id = id;
        FamilyId = familyId;
        Score = score;
    }

    public string Id { get; set; }

    public string FamilyId { get; set; }

    public int Score { get; set; }
}