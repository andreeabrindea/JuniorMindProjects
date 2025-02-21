namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        int currentWindowHeightAvailableForCommits = Console.WindowHeight - 3;
        int totalWidthAvailableSpace = Console.WindowWidth - 5;

        DisplayConfig displayConfig = new(totalWidthAvailableSpace, 5, currentWindowHeightAvailableForCommits);
        var commits = gitClient.GetCommits();
        displayConfig.DisplayCommitsAndPanel(commits, currentWindowHeightAvailableForCommits, 0, currentWindowHeightAvailableForCommits);
        displayConfig.MoveCursor(commits);
    }
}
