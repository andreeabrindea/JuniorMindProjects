namespace GitClientApp;

public class CommitInfo
{
    public CommitInfo(string hash, DateTime date, string author, string message)
    {
        Hash = hash;
        Date = date;
        Author = author;
        Message = message;
    }

    public string Hash { get; set; }

    public string Author { get; set; }

    public DateTime Date { get; set; }

    public string Message { get; set; }
}