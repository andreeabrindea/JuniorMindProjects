namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        const int codeForYellow = 33;
        const int codeForBlue = 34;
        const int codeForMagenta = 35;
        const int codeForGreen = 36;
        foreach (var r in gitClient.GetCommits())
        {
            Console.WriteLine(
                " {0, 10}  {1, 10}  {2, 10}  {3, 10} ",
                $"\x1b[{codeForYellow}m{r.Hash}\x1b[0m",
                $"\x1b[{codeForBlue}m{r.Date}\x1b[0m",
                $"\x1b[{codeForMagenta}m{r.Author}\x1b[0m",
                $"\x1b[{codeForGreen}m{r.Message}");
        }
    }
}
