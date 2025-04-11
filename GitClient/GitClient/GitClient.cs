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
        const string command = "log --pretty=format:\"COMMIT_START%n%h|%H|%an|%ae|%ad|%s|%b\" --name-status --date=iso";
        string output = ExecuteGitCommand(command);
        string[] commitBlocks = output.Split("COMMIT_START", StringSplitOptions.RemoveEmptyEntries);
        List<CommitInfo> commits = new List<CommitInfo>();

        foreach (string commitBlock in commitBlocks)
        {
            if (string.IsNullOrWhiteSpace(commitBlock))
            {
                continue;
            }

            string[] lines = commitBlock.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0)
            {
                continue;
            }

            string[] commitParts = lines[0].Split('|');
            const int indexOfShortHash = 0;
            const int indexOfLongHash = 1;
            const int indexOfAuthor = 2;
            const int indexOfEmail = 3;
            const int indexOfDate = 4;
            const int indexOfMessage = 5;
            const int indexOfMessageBody = 6;

            CommitInfo commit = new CommitInfo(
                commitParts[indexOfShortHash],
                commitParts[indexOfLongHash],
                DateTime.Parse(commitParts[indexOfDate], DateTimeFormatProvider),
                commitParts[indexOfAuthor],
                commitParts[indexOfEmail],
                commitParts[indexOfMessage],
                commitParts.Length > indexOfMessageBody ? commitParts[indexOfMessageBody] : "");

            foreach (var line in lines.Skip(1).ToList())
            {
                var part = line.Split('\t');
                string statusCode = part[0];
                string[] files = part[1].Split('/');
                string directory = "•";
                for (int i = 0; i < files.Length - 1; i++)
                {
                    directory += $"{files[i]}\\";
                }

                if (directory.Length > 2)
                {
                    directory = directory[..^1];
                }

                commit.ListOfModifiedFiles.Add(new ModifiedFile(statusCode, directory, files[^1]));
            }

            commits.Add(commit);
        }

        return commits;
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