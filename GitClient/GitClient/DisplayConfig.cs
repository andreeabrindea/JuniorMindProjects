namespace GitClientApp;

public class DisplayConfig
{
    private const int BorderHeight = 2;
    private int availableWidthSpace = Console.WindowWidth - 2;
    private int windowHeightForCommits;
    private int totalWidth = Console.WindowWidth;
    private bool isRunning;

    public DisplayConfig(List<CommitInfo> commits)
    {
        Commits = commits;
        CurrentLine = 0;
        LowerBound = 0;
        windowHeightForCommits = Console.WindowHeight - BorderHeight;
        UpperBound = windowHeightForCommits > Commits.Count ? Commits.Count : windowHeightForCommits;
        Console.SetWindowSize(totalWidth, windowHeightForCommits + BorderHeight);
        ScrollBarPosition = 0;
        Console.CursorVisible = false;
        isRunning = true;
        var threadToCheckWindowSize = new Thread(UpdateConsoleWindowSizeAfterResize)
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
        Console.SetCursorPosition(0, 0);
        DisplayPanelHeader($"{Commits.Count.ToString()}/{CurrentLine + 1}");
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        const int hashColumnWidth = 8;
        const int dateColumnWidth = 12;
        const int authorColumnWidth = 20;
        for (int i = LowerBound; i < UpperBound; i++)
        {
            string endBackgroundColor = i == CurrentLine ? "\x1b[0m" : "";
            int currentLineLength = hashColumnWidth + dateColumnWidth + authorColumnWidth + spaceBetweenEntries + borderLineCountBefore;
            int messageColumnWidth = Math.Max(availableWidthSpace - currentLineLength, 1);
            currentLineLength += messageColumnWidth;

            DisplayCommitLine(Commits, i, messageColumnWidth);
            FillRemainingWidthSpace(currentLineLength, availableWidthSpace);

            Console.Write(i == ScrollBarPosition ? "\x1b[46m \x1b[0m" : $"{endBackgroundColor}│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter();
    }

    internal void Navigate()
    {
        ConsoleKey readKey;
        do
        {
            readKey = Console.ReadKey(true).Key;
            switch (readKey)
            {
                case ConsoleKey.UpArrow:
                    HandleUpArrowNavigation();
                    UpdateScrollBarPosition();
                    DisplayCommitsAndPanel();
                    break;

                case ConsoleKey.DownArrow:
                    HandleDownArrowNavigation();
                    UpdateScrollBarPosition();
                    DisplayCommitsAndPanel();
                    break;

                case ConsoleKey.PageUp:
                    HandlePageUpNavigation();
                    UpdateScrollBarPosition();
                    DisplayCommitsAndPanel();
                    break;

                case ConsoleKey.PageDown:
                    HandlePageDownNavigation();
                    UpdateScrollBarPosition();
                    DisplayCommitsAndPanel();
                    break;

                case ConsoleKey.Escape:
                    break;

                default:
                    continue;
            }
        }
        while (readKey != ConsoleKey.Escape);
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
    }

    private void HandlePageUpNavigation()
    {
        int step = UpperBound - LowerBound;

        CurrentLine = Math.Max(0, CurrentLine - step);
        if (LowerBound >= step)
        {
            LowerBound -= step;
            UpperBound -= step;
        }
        else
        {
            UpperBound -= LowerBound;
            LowerBound = 0;
        }
    }

    private void HandlePageDownNavigation()
    {
        int step = UpperBound - LowerBound;

        CurrentLine = Math.Min(Commits.Count - 1, CurrentLine + step);
        if (UpperBound == Commits.Count)
        {
            return;
        }

        step = Math.Min(step, Commits.Count - UpperBound);
        LowerBound += step;
        UpperBound += step;
    }

    private void DisplayCommitLine(List<CommitInfo> commits, int i, int messageColumnWidth)
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

        const int hashColumnWidth = 8;
        const int dateColumnWidth = 12;
        const int authorColumnWidth = 20;

        string hash = commits[i].ShortHash.Length > hashColumnWidth
            ? commits[i].ShortHash.Substring(0, hashColumnWidth - 1) + "…"
            : commits[i].ShortHash.PadRight(hashColumnWidth);

        string date = commits[i].Date.Length > dateColumnWidth
            ? commits[i].Date.Substring(0, dateColumnWidth - 1) + "…"
            : commits[i].Date.PadRight(dateColumnWidth);

        string author = commits[i].Author.Length > authorColumnWidth
            ? commits[i].Author.Substring(0, authorColumnWidth - 1) + "…"
            : commits[i].Author.PadRight(authorColumnWidth);

        string message = commits[i].Message.Length > messageColumnWidth
            ? commits[i].Message.Substring(0, messageColumnWidth - 1) + "…"
            : commits[i].Message.PadRight(messageColumnWidth);

        Console.Write(
            "{0} {1} {2} {3} {4}",
            $"│{backgroundColor}",
            $"{colorForHash}{hash}\x1b[0m{backgroundColor}",
            $"{colorForDate}{date}\x1b[0m{backgroundColor}",
            $"{colorForAuthor}{author}\x1b[0m{backgroundColor}",
            $"{message}{backgroundColor}");
    }

    private void DisplayPanelHeader(string message, string color = "")
    {
        const string startCorner = "┌";
        const string endCorner = "┐";
        const string border = "─";
        int currentLineLength = startCorner.Length + endCorner.Length + message.Length;
        Console.Write(color + startCorner + message + "\x1b[0m");
        for (int i = currentLineLength; i < availableWidthSpace; i++)
        {
            Console.Write(color + border + "\x1b[0m");
        }

        Console.Write(color + endCorner + "\x1b[0m");
        Console.WriteLine();
    }

    private void DisplayPanelFooter(string color = "")
    {
        const string border = "─";
        const string startCorner = "└";
        const string endCorner = "┘";
        Console.Write($"{color}{startCorner}\x1b[0m");
        for (int i = startCorner.Length + endCorner.Length; i < availableWidthSpace; i++)
        {
            Console.Write($"{color}{border}\x1b[0m");
        }

        Console.Write($"{color}{endCorner}\x1b[0m");
    }

    private void UpdateConsoleWindowSizeAfterResize()
    {
        bool needsRedraw = false;
        while (isRunning)
        {
            if (Console.WindowWidth != totalWidth)
            {
                totalWidth = Console.WindowWidth;
                availableWidthSpace = Console.WindowWidth - BorderHeight;
                needsRedraw = true;
            }

            if (Console.WindowHeight - BorderHeight != windowHeightForCommits)
            {
                UpdateBoundsAfterWindowHeightResize();
                needsRedraw = true;
            }

            if (needsRedraw)
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                DisplayCommitsAndPanel();
                needsRedraw = false;
            }

            const int timeOut = 50;
            Thread.Sleep(timeOut);
        }
    }

    private void UpdateBoundsAfterWindowHeightResize()
    {
        int differenceBetweenCurrentAndPreviousHeight = Console.WindowHeight - (windowHeightForCommits + BorderHeight);

        if (UpperBound + differenceBetweenCurrentAndPreviousHeight <= Commits.Count &&
            UpperBound + differenceBetweenCurrentAndPreviousHeight > 0)
        {
            UpperBound += differenceBetweenCurrentAndPreviousHeight;
        }
        else
        {
            LowerBound = Math.Max(0, LowerBound - differenceBetweenCurrentAndPreviousHeight);
        }

        windowHeightForCommits = Console.WindowHeight - BorderHeight;
        Console.SetWindowSize(totalWidth, windowHeightForCommits + BorderHeight);

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

    private void UpdateScrollBarPosition() => ScrollBarPosition = LowerBound + (int)((double)CurrentLine / Commits.Count * (UpperBound - LowerBound));
}