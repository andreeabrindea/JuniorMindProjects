using System.Diagnostics;
using System.Text;

namespace GitClientApp;

public class GitClient
{
    private readonly string gitExecutablePath;
    private readonly string workingDirectory;

    public GitClient(string repositoryPath)
    {
        workingDirectory = repositoryPath;

        // TODO: do not override and replace it with the actual path for each possible OS
        gitExecutablePath = "/usr/bin/git";
    }

    public List<CommitInfo> GetCommits()
    {
        List<CommitInfo> commits = new();
        const string command = "log --pretty=format:\"%H,%an,%ad,%s\" --date=iso";
        string output = ExecuteGitCommand(command);
        char[] newLineEscapeCharacters = { '\r', '\n' };
        const int indexOfHash = 0;
        const int indexOfAuthor = 1;
        const int indexOfDate = 2;
        const int indexOfMessage = 3;
        foreach (var line in output.Split(newLineEscapeCharacters, StringSplitOptions.RemoveEmptyEntries))
        {
            try
            {
                string[] entries = line.Split(',', 4);
                commits.Add(new CommitInfo(entries[indexOfHash], entries[indexOfAuthor], DateTime.Parse(entries[indexOfDate]), entries[indexOfMessage]));
            }
            catch (InvalidDataException e)
            {
                throw new InvalidDataException($"Invalid output data format. {e.Message}");
            }
        }

        return commits;
    }

    private string ExecuteGitCommand(string arguments)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = gitExecutablePath,
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