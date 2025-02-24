namespace GitClientApp;

public class DisplayConfig
{
    public DisplayConfig(List<CommitInfo> commits, int availableWidthSpace, int padding, int windowHeightForCommits)
    {
        AvailableWidthSpace = availableWidthSpace;
        Padding = padding;
        WindowHeightForCommits = windowHeightForCommits;
        Commits = commits;
    }

    public int AvailableWidthSpace { get; }

    public int Padding { get; }

    public int WindowHeightForCommits { get; }

    public List<CommitInfo> Commits { get; }

    internal void DisplayCommitsAndPanel(int currentLine, int lowerBound, int upperBound, int scrollBarPosition)
    {
        DisplayPanelHeader(currentLine,  AvailableWidthSpace);
        const int ellipsisLength = 3;
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        for (int i = lowerBound; i < upperBound; i++)
        {
            string endBackgroundColor = i == currentLine ? "\x1b[0m" : "";
            int currentLineLength = Commits[i].Hash.Length + Commits[i].Date.Length + Commits[i].Author.Length +
                                    Commits[i].Message.Length + spaceBetweenEntries + borderLineCountBefore + Padding + 1;

            // Check if the current line length is bigger than the space available and add an ellipsis to mark that the text was trimmed
            if (AvailableWidthSpace < currentLineLength + ellipsisLength)
            {
                int overlapDistance = currentLineLength - AvailableWidthSpace;
                currentLineLength -= Commits[i].Message.Length;
                Commits[i].Message = Commits[i].Message.Length < overlapDistance ? "" : Commits[i].Message.Substring(0, Commits[i].Message.Length - overlapDistance - 1 - ellipsisLength) + "...";
                currentLineLength += Commits[i].Message.Length + ellipsisLength;
            }

            DisplayCommitLine(Commits, i, currentLine);

            // Fill remaining space from the panel with empty spaces
            for (int j = currentLineLength; j < AvailableWidthSpace - 1; j++)
            {
                Console.Write(" ");
            }

            Console.Write(i == scrollBarPosition ? "\x1b[46m \x1b[0m" : $"{endBackgroundColor}│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter(AvailableWidthSpace);
    }

    internal void MoveCursor()
    {
        var currentPosition = WindowHeightForCommits;
        int upperBound = WindowHeightForCommits;
        int lowerBound = 0;
        int scrollIndex = lowerBound;
        var readKey = Console.ReadKey(true).Key;

        while (readKey != ConsoleKey.Escape)
        {
            readKey = Console.ReadKey().Key;

            if (readKey == ConsoleKey.UpArrow)
            {
                HandleUpArrowNavigation(ref currentPosition, ref lowerBound, ref upperBound, ref scrollIndex);
            }
            else if (readKey == ConsoleKey.DownArrow)
            {
                HandleDownArrowNavigation(ref currentPosition, ref lowerBound, ref upperBound, ref scrollIndex);
            }
            else
            {
                continue;
            }

            DisplayCommitsWithUpdatedCursorPosition(Commits, currentPosition, lowerBound, upperBound, scrollIndex);
        }
}

    private void HandleUpArrowNavigation(ref int currentPosition, ref int lowerBound, ref int upperBound, ref int scrollIndex)
    {
        if (currentPosition <= 0)
        {
            return;
        }

        currentPosition--;

        if (currentPosition > upperBound || lowerBound <= 0 || upperBound <= 0)
        {
            return;
        }

        upperBound--;
        lowerBound--;
        scrollIndex = lowerBound + (int)((double)lowerBound / Commits.Count * (upperBound - lowerBound));
    }

    private void HandleDownArrowNavigation(ref int currentPosition, ref int lowerBound, ref int upperBound, ref int scrollIndex)
    {
        if (currentPosition == Commits.Count - 1)
        {
            return;
        }

        if (currentPosition < upperBound)
        {
            currentPosition++;
        }

        if (currentPosition != upperBound || lowerBound >= Commits.Count || upperBound >= Commits.Count)
        {
            return;
        }

        upperBound++;
        lowerBound++;
        scrollIndex = lowerBound + (int)((double)currentPosition / Commits.Count * (upperBound - lowerBound));
    }

    private void DisplayCommitLine(List<CommitInfo> commits, int i, int currentLine)
    {
        string colorForHash = "\x1b[33m";
        string colorForDate = "\x1b[34m";
        string colorForAuthor = "\x1b[35m";
        string backgroundColor = "\x1b[46m";

        backgroundColor = i == currentLine ? backgroundColor : "";
        colorForHash = i == currentLine ? "" : colorForHash;
        colorForDate = i == currentLine ? "" : colorForDate;
        colorForAuthor = i == currentLine ? "" : colorForAuthor;
        colorForHash = i == currentLine ? "" : colorForHash;
        Console.Write(
            "{0} {1} {2} {3} {4} {5}",
            $"│{backgroundColor}",
            $"{colorForHash}{commits[i].Hash}\x1b[0m{backgroundColor}",
            $"{colorForDate}{commits[i].Date}\x1b[0m{backgroundColor}",
            "".PadLeft(Padding),
            $"{colorForAuthor}{commits[i].Author}\x1b[0m{backgroundColor}",
            $"{commits[i].Message}{backgroundColor}");
    }

    private void DisplayCommitsWithUpdatedCursorPosition(List<CommitInfo> commits, int currentPosition, int lowerBound, int upperBound, int scrollBarPosition)
    {
        Console.Clear();
        DisplayCommitsAndPanel(currentPosition, lowerBound, upperBound, scrollBarPosition);
        Console.SetCursorPosition(0, Math.Min(currentPosition + 1, Console.BufferHeight));
    }

    private void DisplayPanelHeader(int currentCommitNumber, int remainingSpace)
    {
        const string startCorner = "┌";
        const string endCorner = "┐";
        const string delimiter = "/";
        const string border = "─";
        int currentLineLength = startCorner.Length + Commits.Count.ToString().Length + delimiter.Length + endCorner.Length + currentCommitNumber.ToString().Length;
        Console.Write(startCorner + (currentCommitNumber + 1) + delimiter + Commits.Count);
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