namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        int currentWindowHeightAvailableForCommits = Console.WindowHeight - 4;
        int totalWidthAvailableSpace = Console.WindowWidth - 1;
        var commits = gitClient.GetCommits();
        DisplayConfig displayConfig = new(commits, totalWidthAvailableSpace, 5, currentWindowHeightAvailableForCommits);
        displayConfig.DisplayCommitsAndPanel(0, 0, currentWindowHeightAvailableForCommits, 0);
        displayConfig.MoveCursor();
    }
}
