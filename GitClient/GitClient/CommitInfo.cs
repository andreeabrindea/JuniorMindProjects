namespace GitClientApp;

public class CommitInfo
{
    public CommitInfo(string hash, string author, DateTime date, string message)
    {
        Hash = hash;
        Author = author;
        Date = date;
        Message = message;
    }

    public string Hash { get; set; }

    public string Author { get; set; }

    public DateTime Date { get; set; }

    public string Message { get; set; }
}