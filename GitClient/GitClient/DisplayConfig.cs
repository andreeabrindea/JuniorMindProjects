namespace GitClientApp;

public class DisplayConfig
{
    public DisplayConfig(int availableWidthSpace, int padding, int windowHeightForCommits)
    {
        AvailableWidthSpace = availableWidthSpace;
        Padding = padding;
        WindowHeightForCommits = windowHeightForCommits;
    }

    public int AvailableWidthSpace { get; }

    public int Padding { get; }

    public int WindowHeightForCommits { get; }

    internal void DisplayCommitsAndPanel(List<CommitInfo> commits, int currentLine, int lowerBound, int upperBound)
    {
        DisplayPanelHeader(commits, currentLine,  AvailableWidthSpace);
        const int ellipsisLength = 3;
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        for (int i = lowerBound; i < upperBound; i++)
        {
            string endBackgroundColor = i == currentLine ? "\x1b[0m" : "";
            int currentLineLength = commits[i].Hash.Length + commits[i].Date.Length + commits[i].Author.Length +
                                    commits[i].Message.Length + spaceBetweenEntries + borderLineCountBefore + Padding + 1;

            // Check if the current line length is bigger than the space available and add an ellipsis to mark that the text was trimmed
            if (AvailableWidthSpace < currentLineLength + ellipsisLength)
            {
                int overlapDistance = currentLineLength - AvailableWidthSpace;
                currentLineLength -= commits[i].Message.Length;
                commits[i].Message = commits[i].Message.Length < overlapDistance ? "" : commits[i].Message.Substring(0, commits[i].Message.Length - overlapDistance - 1 - ellipsisLength) + "...";
                currentLineLength += commits[i].Message.Length + ellipsisLength;
            }

            DisplayCommitLine(commits, i, currentLine);

            // Fill remaining space from the panel with empty spaces
            for (int j = currentLineLength; j < AvailableWidthSpace - 1; j++)
            {
                Console.Write(" ");
            }

            Console.Write($"{endBackgroundColor}│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter(AvailableWidthSpace);
    }

    internal void MoveCursor(List<CommitInfo> commits)
    {
        var readKey = Console.ReadKey(true).Key;
        var currentPosition = WindowHeightForCommits;
        int upperBound = WindowHeightForCommits;
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

    private void DisplayCommitLine(List<CommitInfo> commits, int i, int currentLine)
    {
        const int codeForYellow = 33;
        const int codeForBlue = 34;
        const int codeForMagenta = 35;
        string backgroundColor = i == currentLine ? "\x1b[46m" : "";

        Console.Write(
            "{0} {1} {2} {3} {4} {5}",
            $"│{backgroundColor}",
            $"\x1b[{codeForYellow}m{commits[i].Hash}\x1b[0m{backgroundColor}",
            $"\x1b[{codeForBlue}m{commits[i].Date}\x1b[0m{backgroundColor}",
            "".PadLeft(Padding),
            $"\x1b[{codeForMagenta}m{commits[i].Author}\x1b[0m{backgroundColor}",
            $"{commits[i].Message}{backgroundColor}");
    }

    private void DisplayCommitsWithUpdatedCursorPosition(List<CommitInfo> commits, int currentPosition, int lowerBound, int upperBound)
    {
        Console.Clear();
        DisplayCommitsAndPanel(commits, currentPosition, lowerBound, upperBound);
        Console.SetCursorPosition(0, currentPosition + 1);
    }

    private void DisplayPanelHeader(List<CommitInfo> commits, int currentCommitNumber, int remainingSpace)
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

    private void DisplayPanelFooter(int remainingSpace)
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