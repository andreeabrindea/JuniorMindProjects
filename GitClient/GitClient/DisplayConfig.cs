namespace GitClientApp;

public class DisplayConfig
{
    private const int Padding = 5;
    private const int BorderHeight = 2;
    private int availableWidthSpace = Console.WindowWidth - 1;
    private int windowHeightForCommits = Console.WindowHeight - BorderHeight;
    private int totalWidth = Console.WindowWidth;
    private bool isRunning;

    public DisplayConfig(List<CommitInfo> commits)
    {
        Commits = commits;
        CurrentLine = 0;
        LowerBound = 0;
        UpperBound = windowHeightForCommits;
        ScrollBarPosition = 0;
        Console.CursorVisible = false;
        isRunning = true;
        var threadToCheckWindowSize = new Thread(UpdateConsoleWindowSize)
        {
            IsBackground = true
        };
        threadToCheckWindowSize.Start();
    }

    internal List<CommitInfo> Commits { get; }

    internal int CurrentLine { get; set; }

    internal int ScrollBarPosition { get; set; }

    internal int LowerBound { get; set; }

    internal int UpperBound { get; set; }

    internal void DisplayCommitsAndPanel()
    {
        DisplayPanelHeader();
        const int ellipsisLength = 3;
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        for (int i = LowerBound; i < UpperBound; i++)
        {
            string endBackgroundColor = i == CurrentLine ? "\x1b[0m" : "";
            int currentLineLength = Commits[i].Hash.Length + Commits[i].Date.Length + Commits[i].Author.Length +
                                    Commits[i].Message.Length + spaceBetweenEntries + borderLineCountBefore + Padding + 1;

            // Check if the current line length is bigger than the space available and add an ellipsis to mark that the text was trimmed
            string originalCommitMessage = Commits[i].Message;
            if (availableWidthSpace < currentLineLength + ellipsisLength)
            {
                int overlapDistance = currentLineLength - availableWidthSpace;
                currentLineLength -= Commits[i].Message.Length;
                int newCommitLength = Math.Max(0, Commits[i].Message.Length - overlapDistance - 1 - ellipsisLength);
                Commits[i].Message = newCommitLength == 0 ? "" : Commits[i].Message[..newCommitLength] + "...";
                currentLineLength += Commits[i].Message.Length + ellipsisLength;
            }

            DisplayCommitLine(Commits, i);
            Commits[i].Message = originalCommitMessage;

            // Fill remaining space from the panel with empty spaces
            for (int j = currentLineLength; j < availableWidthSpace - 1; j++)
            {
                Console.Write(" ");
            }

            Console.Write(i == ScrollBarPosition ? "\x1b[46m \x1b[0m" : $"{endBackgroundColor}│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter();
    }

    internal void MoveCursor()
    {
        var readKey = Console.ReadKey(true).Key;

        while (readKey != ConsoleKey.Escape)
        {
            readKey = Console.ReadKey().Key;

            if (readKey == ConsoleKey.UpArrow)
            {
                HandleUpArrowNavigation();
            }
            else if (readKey == ConsoleKey.DownArrow)
            {
                HandleDownArrowNavigation();
            }
            else
            {
                continue;
            }

            DisplayCommitsWithUpdatedCursorPosition();
        }

        isRunning = false;
    }

    private void HandleUpArrowNavigation()
    {
        if (CurrentLine <= 0)
        {
            return;
        }

        CurrentLine--;

        if (CurrentLine > UpperBound || LowerBound <= 0 || UpperBound <= 0)
        {
            return;
        }

        UpperBound--;
        LowerBound--;
        ScrollBarPosition = LowerBound + (int)((double)LowerBound / Commits.Count * (UpperBound - LowerBound));
    }

    private void HandleDownArrowNavigation()
    {
        if (CurrentLine == Commits.Count - 1)
        {
            return;
        }

        if (CurrentLine < UpperBound)
        {
            CurrentLine++;
        }

        if (CurrentLine != UpperBound || LowerBound >= Commits.Count || UpperBound >= Commits.Count)
        {
            return;
        }

        UpperBound++;
        LowerBound++;
        ScrollBarPosition = LowerBound + (int)((double)CurrentLine / Commits.Count * (UpperBound - LowerBound));
    }

    private void DisplayCommitLine(List<CommitInfo> commits, int i)
    {
        string colorForHash = "\x1b[33m";
        string colorForDate = "\x1b[34m";
        string colorForAuthor = "\x1b[35m";
        string backgroundColor = "\x1b[46m";

        backgroundColor = i == CurrentLine ? backgroundColor : "";
        colorForHash = i == CurrentLine ? "" : colorForHash;
        colorForDate = i == CurrentLine ? "" : colorForDate;
        colorForAuthor = i == CurrentLine ? "" : colorForAuthor;
        colorForHash = i == CurrentLine ? "" : colorForHash;
        Console.Write(
            "{0} {1} {2} {3} {4} {5}",
            $"│{backgroundColor}",
            $"{colorForHash}{commits[i].Hash}\x1b[0m{backgroundColor}",
            $"{colorForDate}{commits[i].Date}\x1b[0m{backgroundColor}",
            $"{colorForAuthor}{commits[i].Author}\x1b[0m{backgroundColor}",
            "".PadLeft(Padding),
            $"{commits[i].Message}{backgroundColor}");
    }

    private void DisplayCommitsWithUpdatedCursorPosition()
    {
        Console.Clear();
        DisplayCommitsAndPanel();
    }

    private void DisplayPanelHeader()
    {
        const string startCorner = "┌";
        const string endCorner = "┐";
        const string delimiter = "/";
        const string border = "─";
        int currentLineLength = startCorner.Length + Commits.Count.ToString().Length + delimiter.Length + endCorner.Length + (CurrentLine + 1).ToString().Length;
        Console.Write(startCorner + (CurrentLine + 1) + delimiter + Commits.Count);
        for (int i = currentLineLength; i < availableWidthSpace - 1; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
        Console.WriteLine();
    }

    private void DisplayPanelFooter()
    {
        const string border = "─";
        const string startCorner = "└";
        const string endCorner = "┘";
        Console.Write(startCorner);
        for (int i = startCorner.Length + endCorner.Length; i < availableWidthSpace - 1; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
    }

    private void UpdateConsoleWindowSize()
    {
        while (isRunning)
        {
            if (Console.WindowWidth != totalWidth)
            {
                totalWidth = Console.WindowWidth;
                availableWidthSpace = Console.WindowWidth - 1;
                Console.Clear();
                DisplayCommitsAndPanel();
            }

            if (Console.WindowHeight - BorderHeight != windowHeightForCommits)
            {
                UpdateBounds();
                Console.Clear();
                DisplayCommitsAndPanel();
            }

            const int timeOut = 100;
            Thread.Sleep(timeOut);
        }
    }

    private void UpdateBounds()
    {
        int difference = Console.WindowHeight - BorderHeight - windowHeightForCommits;
        if (UpperBound + difference >= 0 && UpperBound + difference <= Commits.Count)
        {
            UpperBound += difference;
        }

        windowHeightForCommits = Console.WindowHeight - BorderHeight;
        if (CurrentLine >= UpperBound)
        {
            CurrentLine = UpperBound - 1;
        }

        if (CurrentLine >= LowerBound)
        {
            return;
        }

        CurrentLine = LowerBound + 1;
    }
}