namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        foreach (var r in gitClient.GetCommits())
        {
            Console.WriteLine(r.Author);
        }
    }
}
