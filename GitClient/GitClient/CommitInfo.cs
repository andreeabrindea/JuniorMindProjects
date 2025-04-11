namespace GitClientApp;

public class CommitInfo
{
    public CommitInfo(string shortHash, string longHash, DateTime date, string author, string email, string message, string messageBody)
    {
        ShortHash = shortHash;
        LongHash = longHash;
        Date = date.ToString("dd/MM/yyyy");
        LongDate = date.ToString("dd/MM/yyyy HH:mm");
        Author = author;
        Message = message;
        Email = email;
        MessageBody = messageBody;
        ListOfModifiedFiles = new List<ModifiedFile>();
    }

    public string ShortHash { get; set; }

    public string LongHash { get; set; }

    public string Author { get; set; }

    public string Date { get; set; }

    public string LongDate { get; set; }

    public string Message { get; set; }

    public string Email { get; set; }

    public string MessageBody { get; set; }

    public List<ModifiedFile> ListOfModifiedFiles { get; internal set; }
}