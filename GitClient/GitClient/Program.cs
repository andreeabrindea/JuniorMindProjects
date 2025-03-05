namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        int currentWindowHeightAvailableForCommits = Console.WindowHeight - 4;
        var commits = gitClient.GetCommits();
        DisplayConfig displayConfig = new(commits);
        displayConfig.DisplayCommitsAndPanel();
        displayConfig.MoveCursor();
    }
}
