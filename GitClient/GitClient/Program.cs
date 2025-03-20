namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        var commits = gitClient.GetCommits();
        DisplayConfig displayConfig = new(commits);
        displayConfig.DisplayCommitsAndPanel();
        displayConfig.Navigate();
    }
}
