using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace GitClientApp;

public class GitClient
{
    private static readonly IFormatProvider DateTimeFormatProvider = CultureInfo.CurrentCulture;
    private readonly string workingDirectory;

    public GitClient(string repositoryPath)
    {
        workingDirectory = repositoryPath;
    }

    public List<CommitInfo> GetCommits()
    {
        const string command = "log --pretty=format:\"%h|%H|%an|%ae|%ad|%s|%b\" --date=iso";
        string output = ExecuteGitCommand(command);
        char[] newLineEscapeCharacters = { '\r', '\n' };
        const int indexOfShortHash = 0;
        const int indexOfLongHash = 1;
        const int indexOfAuthor = 2;
        const int indexOfEmail = 3;
        const int indexOfDate = 4;
        const int indexOfMessage = 5;
        const int indexOfMessageBody = 6;

        return output.Split(newLineEscapeCharacters, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split('|'))
            .Select(entries =>
                new CommitInfo(
                    entries[indexOfShortHash],
                    entries[indexOfLongHash],
                    DateTime.Parse(entries[indexOfDate], DateTimeFormatProvider),
                    entries[indexOfAuthor],
                    entries[indexOfEmail],
                    entries[indexOfMessage],
                    entries[indexOfMessageBody])).ToList();
    }

    private string ExecuteGitCommand(string arguments)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "git",
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };

        using Process process = new();
        process.StartInfo = startInfo;
        StringBuilder output = new();
        StringBuilder error = new();

        process.OutputDataReceived += (_, e) =>
        {
            if (e.Data == null)
            {
                return;
            }

            output.AppendLine(e.Data);
        };

        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data == null)
            {
                return;
            }

            error.AppendLine(e.Data);
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new InvalidOperationException($"Git command failed: {error}");
            }

            return output.ToString();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to execute git command: {ex.Message}");
        }
    }
}