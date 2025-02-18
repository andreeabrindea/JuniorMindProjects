﻿namespace GitClientApp;

internal class Program
{
    public static void Main(string[] args)
    {
        GitClient gitClient = new(Directory.GetCurrentDirectory());
        var commits = gitClient.GetCommits();
        DisplayCommits(commits, commits.Count, Console.WindowHeight);
        MoveCursor(commits);
    }

    private static void DisplayCommits(List<CommitInfo> commits, int currentLine, int limit)
    {
        int remainingSpace = Console.WindowWidth - 5;
        DisplayPanelHeader(commits, currentLine + 1,  remainingSpace);
        const int codeForYellow = 33;
        const int codeForBlue = 34;
        const int codeForMagenta = 35;
        const int padRight = 28;
        const int spacesBetween = 2 * 3 + 2 + 6;
        const int borderLength = 3;
        const int three = 3;
        for (int i = 0; i < limit; i++)
        {
            if (i == currentLine)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }

            int currentLineLength = commits[i].Hash.Length + commits[i].Date.Length + commits[i].Author.Length + commits[i].Message.Length + spacesBetween + borderLength;
            if (remainingSpace < currentLineLength + three)
            {
                int overlapDistance = currentLineLength - remainingSpace;
                commits[i].Message = commits[i].Message.Length < overlapDistance ? "" : commits[i].Message.Substring(0, commits[i].Message.Length - overlapDistance - 1 - three) + "...";
            }

            Console.Write(
                " {0} {1, 4} {2, 7} {3} {4} {5}",
                "│",
                i + 1,
                $"\x1b[{codeForYellow}m{commits[i].Hash}\x1b[0m",
                $"\x1b[{codeForBlue}m{commits[i].Date}\x1b[0m",
                $"\x1b[{codeForMagenta}m{commits[i].Author}\x1b[0m".PadRight(padRight),
                commits[i].Message);
            for (int j = currentLineLength; j < remainingSpace - 1; j++)
            {
                Console.Write(" ");
            }

            Console.Write("│");
            Console.WriteLine();
            Console.ResetColor();
        }

        DisplayPanelFooter(remainingSpace);
    }

    private static void MoveCursor(List<CommitInfo> commits)
    {
        var readKey = Console.ReadKey(true).Key;
        var currentPosition = Console.GetCursorPosition().Top;
        const int left = 0;
        while (readKey != ConsoleKey.Escape)
        {
            readKey = Console.ReadKey().Key;
            if (readKey == ConsoleKey.UpArrow)
            {
                currentPosition--;
                Console.Clear();
                DisplayCommits(commits, currentPosition, Console.WindowHeight);
                Console.SetCursorPosition(left, currentPosition - 1);
            }

            if (readKey == ConsoleKey.DownArrow)
            {
                currentPosition++;
                Console.Clear();
                DisplayCommits(commits, currentPosition, Console.WindowHeight);
                Console.SetCursorPosition(left, currentPosition + 1);
            }
        }
    }

    private static void DisplayPanelHeader(List<CommitInfo> commits, int currentCommitNumber, int remainingSpace)
    {
        const string startCorner = " ┌";
        const string endCorner = "┐";
        const string delimiter = "/";
        const string border = "─";
        int currentLineLength = startCorner.Length + commits.Count.ToString().Length + delimiter.Length + endCorner.Length + currentCommitNumber.ToString().Length;
        Console.Write(startCorner + commits.Count + delimiter + currentCommitNumber);
        for (int i = currentLineLength; i < remainingSpace; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
        Console.WriteLine();
    }

    private static void DisplayPanelFooter(int remainingSpace)
    {
        const string border = "─";
        const string startCorner = " └";
        const string endCorner = "┘";
        Console.Write(startCorner);
        for (int i = startCorner.Length + endCorner.Length; i < remainingSpace - 1; i++)
        {
            Console.Write(border);
        }

        Console.Write(endCorner);
    }
}
