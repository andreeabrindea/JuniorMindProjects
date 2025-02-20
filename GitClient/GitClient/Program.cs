namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        var commits = gitClient.GetCommits();
        int currentWindowHeightAvailableForCommits = Console.WindowHeight - 3;
        DisplayCommitsAndPanel(commits, currentWindowHeightAvailableForCommits, 0, currentWindowHeightAvailableForCommits);
        MoveCursor(commits, currentWindowHeightAvailableForCommits);
    }

    private static void DisplayCommitsAndPanel(List<CommitInfo> commits, int currentLine, int lowerBound, int upperBound)
    {
        int availableSpace = Console.WindowWidth - 5;
        DisplayPanelHeader(commits, currentLine,  availableSpace);
        const int ellipsisLength = 3;
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        for (int i = lowerBound; i < upperBound; i++)
        {
            string endBackgroundColor = i == currentLine ? "\x1b[0m" : "";
            int currentLineLength = commits[i].Hash.Length + commits[i].Date.Length + commits[i].Author.Length +
                                    commits[i].Message.Length + spaceBetweenEntries + borderLineCountBefore;

            // Check if the current line length is bigger than the space available and add an ellipsis to mark that the text was trimmed
            if (availableSpace < currentLineLength + ellipsisLength)
            {
                int overlapDistance = currentLineLength - availableSpace;
                currentLineLength -= commits[i].Message.Length;
                commits[i].Message = commits[i].Message.Length < overlapDistance ? "" : commits[i].Message.Substring(0, commits[i].Message.Length - overlapDistance - 1 - ellipsisLength) + "...";
                currentLineLength += commits[i].Message.Length + ellipsisLength;
            }

            DisplayCommitLine(commits, i, currentLine);

            // Fill remaining space from the panel with empty spaces
            for (int j = currentLineLength; j < availableSpace - 1; j++)
            {
                Console.Write(" ");
            }

            Console.Write($"{endBackgroundColor}│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter(availableSpace);
    }

    private static void DisplayCommitLine(List<CommitInfo> commits, int i, int currentLine)
    {
        const int codeForYellow = 33;
        const int codeForBlue = 34;
        const int codeForMagenta = 35;
        string backgroundColor = i == currentLine ? "\x1b[46m" : "";

        Console.Write(
            "{0} {1} {2} {3} {4}",
            $"│{backgroundColor}",
            $"\x1b[{codeForYellow}m{commits[i].Hash}\x1b[0m{backgroundColor}",
            $"\x1b[{codeForBlue}m{commits[i].Date}\x1b[0m{backgroundColor}",
            $"\x1b[{codeForMagenta}m{commits[i].Author}\x1b[0m{backgroundColor}",
            $"{commits[i].Message}{backgroundColor}");
    }

    private static void MoveCursor(List<CommitInfo> commits, int availableSpace)
    {
        var readKey = Console.ReadKey(true).Key;
        var currentPosition = availableSpace - 1;
        int upperBound = availableSpace - 1;
        int lowerBound = 0;
        while (readKey != ConsoleKey.Escape)
        {
            readKey = Console.ReadKey().Key;
            switch (readKey)
            {
                case ConsoleKey.UpArrow when currentPosition > 0:
                    currentPosition--;
                    if (currentPosition <= upperBound && lowerBound > 0 && upperBound > 0)
                    {
                        upperBound--;
                        lowerBound--;
                    }

                    break;

                case ConsoleKey.DownArrow when currentPosition < commits.Count:
                    if (currentPosition < upperBound)
                    {
                        currentPosition++;
                    }

                    if (currentPosition == upperBound && lowerBound < commits.Count && upperBound < commits.Count)
                    {
                        upperBound++;
                        lowerBound++;
                    }

                    break;

                default:
                    continue;
            }

            DisplayCommitsWithUpdatedCursorPosition(commits, currentPosition, lowerBound, upperBound);
        }
    }

    private static void DisplayCommitsWithUpdatedCursorPosition(List<CommitInfo> commits, int currentPosition, int lowerBound, int upperBound)
    {
        Console.Clear();
        DisplayCommitsAndPanel(commits, currentPosition, lowerBound, upperBound);
        Console.SetCursorPosition(0, currentPosition + 1);
    }

    private static void DisplayPanelHeader(List<CommitInfo> commits, int currentCommitNumber, int remainingSpace)
    {
        const string startCorner = "┌";
        const string endCorner = "┐";
        const string delimiter = "/";
        const string border = "─";
        int currentLineLength = startCorner.Length + commits.Count.ToString().Length + delimiter.Length + endCorner.Length + currentCommitNumber.ToString().Length;
        Console.Write(startCorner + currentCommitNumber + delimiter + commits.Count);
        for (int i = currentLineLength; i < remainingSpace - 1; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
        Console.WriteLine();
    }

    private static void DisplayPanelFooter(int remainingSpace)
    {
        const string border = "─";
        const string startCorner = "└";
        const string endCorner = "┘";
        Console.Write(startCorner);
        for (int i = startCorner.Length + endCorner.Length; i < remainingSpace - 1; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
    }
}
