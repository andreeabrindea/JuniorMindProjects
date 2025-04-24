using System.Diagnostics;
using System.Text;

namespace GitClientApp;

public class DisplayConfig
{
    private const int NumberOfBorders = 2;
    private int firstColumnWidth;
    private int secondColumnWidth;
    private int height;
    private int totalWidth = Console.WindowWidth;
    private bool isRunning;
    private bool isSecondColumnShown;
    private int rowNumberInSecondColumn;

    public DisplayConfig(List<CommitInfo> commits)
    {
        Commits = commits;
        CurrentLine = 0;
        LowerBound = 0;
        height = Console.WindowHeight - NumberOfBorders;
        UpperBound = height > Commits.Count ? Commits.Count : height;
        Console.SetWindowSize(totalWidth, height + NumberOfBorders);
        ScrollBarPosition = 0;
        Console.CursorVisible = false;
        secondColumnWidth = 0;
        firstColumnWidth = totalWidth;
        isRunning = true;
        var threadToCheckWindowSize = new Thread(UpdateConsoleWindowSizeAfterResize)
        {
            IsBackground = true
        };
        threadToCheckWindowSize.Start();
        isSecondColumnShown = false;
    }

    internal List<CommitInfo> Commits { get; }

    internal int CurrentLine { get; set; }

    internal int ScrollBarPosition { get; set; }

    internal int LowerBound { get; set; }

    internal int UpperBound { get; set; }

    internal void DisplayCommitsAndPanel()
    {
        Console.SetCursorPosition(0, 0);
        DisplayPanelHeader($"{Commits.Count.ToString()}/{CurrentLine + 1}", firstColumnWidth);
        const int spaceBetweenEntries = 5;
        const int borderLineCountBefore = 1;
        const int hashColumnWidth = 8;
        const int dateColumnWidth = 12;
        const int authorColumnWidth = 20;
        for (int i = LowerBound; i < UpperBound; i++)
        {
            string endBackgroundColor = i == CurrentLine ? "\x1b[0m" : "";
            int currentLineLength = hashColumnWidth + dateColumnWidth + authorColumnWidth + spaceBetweenEntries + borderLineCountBefore;
            int messageColumnWidth = Math.Max(firstColumnWidth - currentLineLength, 1);
            currentLineLength += messageColumnWidth;

            DisplayCommitLine(Commits, i, messageColumnWidth);
            FillRemainingWidthSpace(currentLineLength, firstColumnWidth);

            Console.Write(i == ScrollBarPosition ? "\x1b[46m \x1b[0m" : $"{endBackgroundColor}│");
            Console.WriteLine();
        }

        DisplayPanelFooter(firstColumnWidth);
        if (!isSecondColumnShown)
        {
            return;
        }

        Console.SetCursorPosition(firstColumnWidth + 1, 0);
        DisplayCommitInfoPanel();
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

                case ConsoleKey.Enter:
                    isSecondColumnShown = true;
                    Console.Clear();
                    DivideColumnsWidth();
                    DisplayCommitsAndPanel();
                    break;

                case ConsoleKey.Escape:
                    isRunning = false;
                    break;

                default:
                    continue;
            }
        }
        while (readKey != ConsoleKey.Escape);
    }

    private void DivideColumnsWidth()
    {
        const int half = 2;
        firstColumnWidth = isSecondColumnShown ? (totalWidth - 1) / half : totalWidth;
        secondColumnWidth = isSecondColumnShown ? totalWidth - firstColumnWidth - 1 : 0;
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

    private void DisplayPanelHeader(string message, int space, string color = "")
    {
        const string startCorner = "┌";
        const string endCorner = "┐";
        const string border = "─";
        int currentLineLength = startCorner.Length + endCorner.Length + message.Length;
        Console.Write(color + startCorner + message + "\x1b[0m");
        for (int i = currentLineLength; i < space; i++)
        {
            Console.Write(color + border + "\x1b[0m");
        }

        Console.Write(color + endCorner + "\x1b[0m");
        Console.WriteLine();
    }

    private void DisplayPanelFooter(int space, string color = "")
    {
        const string border = "─";
        const string startCorner = "└";
        const string endCorner = "┘";
        Console.Write($"{color}{startCorner}\x1b[0m");
        for (int i = startCorner.Length + endCorner.Length; i < space; i++)
        {
            Console.Write($"{color}{border}\x1b[0m");
        }

        Console.Write($"{color}{endCorner}\x1b[0m");
    }

    private void UpdateConsoleWindowSizeAfterResize()
    {
        while (isRunning)
        {
            if (Console.WindowWidth != totalWidth)
            {
                totalWidth = Console.WindowWidth;
                if (isSecondColumnShown)
                {
                    DivideColumnsWidth();
                }
                else
                {
                    firstColumnWidth = totalWidth;
                    secondColumnWidth = 0;
                }

                Console.Clear();
                DisplayCommitsAndPanel();
            }

            if (Console.WindowHeight - NumberOfBorders != height)
            {
                UpdateBoundsAfterWindowHeightResize();
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                Console.SetCursorPosition(0, 0);
                DisplayCommitsAndPanel();
            }

            const int timeOut = 50;
            Thread.Sleep(timeOut);
        }
    }

    private void UpdateBoundsAfterWindowHeightResize()
    {
        int differenceBetweenCurrentAndPreviousHeight = Console.WindowHeight - (height + NumberOfBorders);

        if (UpperBound + differenceBetweenCurrentAndPreviousHeight <= Commits.Count &&
            UpperBound + differenceBetweenCurrentAndPreviousHeight > 0)
        {
            UpperBound += differenceBetweenCurrentAndPreviousHeight;
        }
        else
        {
            LowerBound = Math.Max(0, LowerBound - differenceBetweenCurrentAndPreviousHeight);
        }

        height = Console.WindowHeight - NumberOfBorders;
        Console.SetWindowSize(totalWidth, height + NumberOfBorders);

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

    private void DisplayCommitInfoPanel()
    {
        int remainingSpace = firstColumnWidth + 1;
        Console.SetCursorPosition(remainingSpace, 0);
        ClearSecondColumn();
        DisplayInfoSubPanel();
        DisplayMessageSubPanel();
    }

    private void FillRemainingWidthSpace(int currentLineLength, int space)
    {
        for (int i = currentLineLength; i < space; i++)
        {
            Console.Write(" ");
        }
    }

    private void DisplayRightBorder(int currentLineLength, int space, string color = "")
    {
        FillRemainingWidthSpace(currentLineLength, space);
        Console.Write($"{color}│");
        Console.WriteLine();
    }

    private void DisplayInfoSubPanel()
    {
        rowNumberInSecondColumn = 0;
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        const string lightGray = "\x1b[90m";

        DisplayPanelHeader("Info", secondColumnWidth, lightGray);
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        Console.Write($"{lightGray}│Author: \x1b[0m{Commits[CurrentLine].Author} <{Commits[CurrentLine].Email}>");
        int currentLineLength = "│Author: ".Length + Commits[CurrentLine].Author.Length + Commits[CurrentLine].Email.Length +
                            " <>".Length;
        DisplayRightBorder(currentLineLength, secondColumnWidth - 1, lightGray);

        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        Console.Write($"{lightGray}│Date: \x1b[0m{Commits[CurrentLine].LongDate}");
        currentLineLength = "│Date: ".Length + Commits[CurrentLine].LongDate.Length;
        DisplayRightBorder(currentLineLength, secondColumnWidth - 1, lightGray);

        currentLineLength = "│Sah: ".Length + Commits[CurrentLine].LongHash.Length;
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        Console.Write($"{lightGray}│Sah: \x1b[0m{Commits[CurrentLine].LongHash}");
        DisplayRightBorder(currentLineLength, secondColumnWidth - 1, lightGray);
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        DisplayPanelFooter(secondColumnWidth, lightGray);
    }

    private void DisplayMessageSubPanel()
    {
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        Console.WriteLine();
        const string lightGray = "\x1b[90m";
        const string boldFontStyle = "\x1b[1m";

        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        rowNumberInSecondColumn++;
        DisplayPanelHeader("Message [..]", secondColumnWidth, lightGray);

        string formattedMessage = AddSideBordersToText(Commits[CurrentLine].Message, lightGray, boldFontStyle);
        string[] lines = formattedMessage.Split("$REPOSITION$");
        foreach (string line in lines)
        {
            Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
            Console.Write(line);
            rowNumberInSecondColumn++;
        }

        if (Commits[CurrentLine].MessageBody.Length > 0)
        {
            Console.Write($"{lightGray}│\x1b[0m");
            DisplayRightBorder(1, secondColumnWidth - 1, lightGray);

            string formattedBody = AddSideBordersToText(Commits[CurrentLine].MessageBody, lightGray);
            lines = formattedBody.Split("$REPOSITION$");

            foreach (string line in lines)
            {
                Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
                Console.Write($"{line}");
                rowNumberInSecondColumn++;
            }
        }

        DisplayPanelFooter(secondColumnWidth, lightGray);
        rowNumberInSecondColumn++;
        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        DisplayModifiedFiles();
    }

    private void DisplayModifiedFiles()
    {
        const int directoryPadding = 2;
        const string lightGray = "\x1b[90m";
        string color = "";
        DisplayPanelHeader($"Files: {Commits[CurrentLine].ListOfModifiedFiles.Count}", secondColumnWidth, lightGray);
        rowNumberInSecondColumn++;

        foreach (var group in Commits[CurrentLine].ListOfModifiedFiles.GroupBy(c => c.Directory))
        {
            Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
            Console.Write($"{lightGray}│\x1b[0m  {group.Key}");
            DisplayRightBorder($"│{group.Key}".Length + directoryPadding, secondColumnWidth - 1, lightGray);
            rowNumberInSecondColumn++;
            foreach (var file in group)
            {
                Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
                switch (file.StatusCode[0])
                {
                    case 'M':
                        color = "\x1b[38;5;220m";
                        break;
                    case 'A':
                        color = "\x1b[38;5;28m";
                        break;
                    case 'R':
                        color = "\x1b[38;5;214m";
                        break;
                    case 'D':
                        color = "\x1b[38;5;9m";
                        break;
                    default:
                        color = "";
                        break;
                }

                int currentLineLength;
                if (file.StatusCode.StartsWith('R'))
                {
                    Console.Write($"{lightGray}│\x1b[0m{color}{file.StatusCode[0]}    \x1B[4m{file.PreviousName}\x1B[24m -> {file.FileName}\x1b[0m");
                    currentLineLength = $"│{file.StatusCode[0]} {file.PreviousName} -> {file.FileName}".Length +
                                        $"│{file.StatusCode[0]} {file.PreviousName} -> {file.FileName}".Count(char.IsWhiteSpace);
                }
                else
                {
                    Console.Write($"{lightGray}│\x1b[0m{color}{file.StatusCode}    {file.FileName}\x1b[0m");
                    currentLineLength = $"│{file.StatusCode}{file.FileName}".Length + $"{lightGray}│\x1b[0m{color}{file.StatusCode}    {file.FileName}\x1b[0m".Count(char.IsWhiteSpace);
                }

                DisplayRightBorder(currentLineLength, secondColumnWidth - 1, lightGray);
                rowNumberInSecondColumn++;
            }
        }

        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        while (rowNumberInSecondColumn <= height)
        {
            Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
            Console.Write('│');
            DisplayRightBorder(1, secondColumnWidth - 1, lightGray);
            rowNumberInSecondColumn++;
        }

        Console.SetCursorPosition(firstColumnWidth + 1, rowNumberInSecondColumn);
        DisplayPanelFooter(secondColumnWidth, lightGray);
    }

    private string AddSideBordersToText(string initialMessage, string borderColor = "", string fontStyle = "")
    {
        StringBuilder result = new StringBuilder();
        int charsPerLine = secondColumnWidth - NumberOfBorders;
        int numberOfLines = (int)Math.Ceiling((double)initialMessage.Length / charsPerLine);

        result.Append($"{borderColor}│\x1b[0m");
        for (int j = 0; j < numberOfLines; j++)
        {
            for (int i = 0; i < Math.Min(charsPerLine, initialMessage.Length); i++)
            {
                result.Append($"{fontStyle}{initialMessage[i]}\x1b[21m");
            }

            if (charsPerLine > initialMessage.Length - 1)
            {
                for (int k = 0; k < charsPerLine - initialMessage.Length; k++)
                {
                    result.Append(" ");
                }
            }

            result.Append($"{borderColor}│\x1b[0m");
            result.AppendLine();
            result.Append("$REPOSITION$");
            initialMessage = initialMessage[Math.Min(charsPerLine, initialMessage.Length)..];
            if (initialMessage.Length > 0)
            {
                result.Append($"{borderColor}│\x1b[0m");
            }
        }

        return result.ToString();
    }

    private void ClearSecondColumn()
    {
        Console.SetCursorPosition(firstColumnWidth + 1, 1);
        for (int row = 1; row < Console.WindowHeight; row++)
        {
            string emptySpaces = "";
            for (int column = 0; column <= secondColumnWidth; column++)
            {
                emptySpaces += " ";
            }

            Console.Write($"\x1B[{row};{firstColumnWidth + 1}H{emptySpaces}");
            Console.WriteLine();
        }
    }
}