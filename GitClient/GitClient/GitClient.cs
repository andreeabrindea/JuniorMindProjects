using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace GitClientApp;

public class GitClient
{
    private readonly string gitExecutablePath;
    private readonly string workingDirectory;

    public GitClient(string repositoryPath)
    {
        workingDirectory = repositoryPath;
        gitExecutablePath = FindGitExecutable();
    }

    public List<CommitInfo> GetCommits()
    {
        const string command = "log --pretty=format:\"%H,%an,%ad,%s\" --date=iso";
        string output = ExecuteGitCommand(command);
        char[] newLineEscapeCharacters = { '\r', '\n' };
        const int indexOfHash = 0;
        const int indexOfAuthor = 1;
        const int indexOfDate = 2;
        const int indexOfMessage = 3;

        return output.Split(newLineEscapeCharacters, StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(','))
            .Select(entries =>
                new CommitInfo(entries[indexOfHash], entries[indexOfAuthor], DateTime.Parse(entries[indexOfDate]), entries[indexOfMessage])).ToList();
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

    private string FindGitExecutable()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            return CheckIfGitExecutableExistsInCommonPaths(new[] { "/usr/bin/git", "/usr/local/bin/git" });
        }

        return CheckIfGitExecutableExistsInCommonPaths(new[]
        {
            @"C:\Program Files\Git\bin\git.exe", @"C:\Program Files (x86)\Git\bin\git.exe"
        });
    }

    private string CheckIfGitExecutableExistsInCommonPaths(string[] commonPaths)
        => commonPaths.FirstOrDefault(File.Exists)
           ?? throw new InvalidOperationException("Git is not installed on the machine.");
}