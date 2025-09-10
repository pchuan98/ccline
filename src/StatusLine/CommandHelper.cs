using System.Diagnostics;
using System.Text;

namespace StatusLine;

/// <summary>
/// Non-blocking command execution helper for status line applications
/// </summary>
public static class CommandHelper
{
    /// <summary>
    /// Execute a command asynchronously and return the result
    /// </summary>
    /// <param name="command">Command string to execute</param>
    /// <param name="workingDirectory">Working directory for the command (optional)</param>
    /// <param name="timeoutMs">Timeout in milliseconds (default: 5000ms)</param>
    /// <returns>Command output string</returns>
    public static async Task<string> ExecuteAsync(string command, string? workingDirectory = null, int timeoutMs = 5000)
    {
        try
        {
            var processInfo = CreateProcessStartInfo(command, workingDirectory);

            using var process = new Process { StartInfo = processInfo };
            var outputBuilder = new StringBuilder();
            var errorBuilder = new StringBuilder();

            // Setup output and error data handlers
            process.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                    outputBuilder.AppendLine(args.Data);
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                    errorBuilder.AppendLine(args.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Wait for process to complete with timeout
            var completed = await WaitForProcessAsync(process, timeoutMs);

            if (!completed)
            {
                try
                {
                    process.Kill();
                    return $"Command timed out after {timeoutMs}ms";
                }
                catch
                {
                    return $"Command timed out and failed to terminate";
                }
            }

            var output = outputBuilder.ToString().Trim();
            var error = errorBuilder.ToString().Trim();

            // Return output if available, otherwise return error, or success message
            if (!string.IsNullOrEmpty(output))
                return output;
            if (!string.IsNullOrEmpty(error))
                return $"Error: {error}";

            return process.ExitCode == 0 ? "Command completed successfully" : $"Command failed with exit code {process.ExitCode}";
        }
        catch (Exception ex)
        {
            return $"Exception: {ex.Message}";
        }
    }

    /// <summary>
    /// Execute a command synchronously (non-blocking for caller but blocking internally)
    /// </summary>
    /// <param name="command">Command string to execute</param>
    /// <param name="workingDirectory">Working directory for the command (optional)</param>
    /// <param name="timeoutMs">Timeout in milliseconds (default: 5000ms)</param>
    /// <returns>Command output string</returns>
    public static string Execute(string command, string? workingDirectory = null, int timeoutMs = 5000)
    {
        return ExecuteAsync(command, workingDirectory, timeoutMs).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Execute a command and return only the first line of output (useful for status lines)
    /// </summary>
    /// <param name="command">Command string to execute</param>
    /// <param name="workingDirectory">Working directory for the command (optional)</param>
    /// <param name="timeoutMs">Timeout in milliseconds (default: 3000ms)</param>
    /// <returns>First line of command output</returns>
    public static string ExecuteFirstLine(string command, string? workingDirectory = null, int timeoutMs = 3000)
    {
        var result = Execute(command, workingDirectory, timeoutMs);
        var firstLine = result.Split('\n', '\r').FirstOrDefault()?.Trim();
        return string.IsNullOrEmpty(firstLine) ? "No output" : firstLine;
    }

    /// <summary>
    /// Check if a command/tool is available in the system PATH
    /// </summary>
    /// <param name="command">Command name to check</param>
    /// <returns>True if command is available</returns>
    public static bool IsCommandAvailable(string command)
    {
        try
        {
            var whichCommand = Environment.OSVersion.Platform == PlatformID.Win32NT ? "where" : "which";
            var result = ExecuteFirstLine($"{whichCommand} {command}", timeoutMs: 2000);
            return !result.Contains("not found") && !result.Contains("No output") && !result.Contains("Error");
        }
        catch
        {
            return false;
        }
    }

    private static ProcessStartInfo CreateProcessStartInfo(string command, string? workingDirectory)
    {
        var isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        return new ProcessStartInfo
        {
            FileName = isWindows ? "cmd.exe" : "/bin/bash",
            Arguments = isWindows ? $"/c {command}" : $"-c \"{command}\"",
            WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
    }

    private static async Task<bool> WaitForProcessAsync(Process process, int timeoutMs)
    {
        var tcs = new TaskCompletionSource<bool>();

        process.Exited += (sender, args) => tcs.TrySetResult(true);
        process.EnableRaisingEvents = true;

        if (process.HasExited)
        {
            return true;
        }

        var timeoutTask = Task.Delay(timeoutMs);
        var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

        return completedTask == tcs.Task;
    }

    // Default Commands

    /// <summary>
    /// Get current git branch name, returns null if not in git repo or no git
    /// </summary>
    /// <returns>Current git branch name or null</returns>
    public static string? GetGitBranch()
    {
        if (!IsCommandAvailable("git"))
            return null;

        try
        {
            var result = ExecuteFirstLine("git branch --show-current", timeoutMs: 2000);
            return string.IsNullOrEmpty(result) || result.Contains("Error") || result.Contains("not a git repository")
                ? null
                : result;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get comprehensive git status information
    /// </summary>
    /// <returns>Git status information object</returns>
    public static GitStatusInfo GetGitStatus()
    {
        var info = new GitStatusInfo();

        if (!IsCommandAvailable("git"))
            return info;

        try
        {
            // Get branch information
            var branchResult = ExecuteFirstLine("git branch --show-current", timeoutMs: 2000);
            if (!branchResult.Contains("Error") && !branchResult.Contains("not a git repository"))
            {
                info.BranchName = string.IsNullOrEmpty(branchResult) ? null : branchResult;
            }

            // Check if detached HEAD
            var headResult = ExecuteFirstLine("git symbolic-ref -q HEAD", timeoutMs: 2000);
            info.IsDetachedHead = headResult.Contains("Error") || string.IsNullOrEmpty(headResult);

            // Get upstream branch information
            var upstreamResult = ExecuteFirstLine("git rev-parse --abbrev-ref @{upstream} 2>nul", timeoutMs: 2000);
            if (!upstreamResult.Contains("Error") && !upstreamResult.Contains("No output"))
            {
                info.UpstreamBranch = upstreamResult;
                info.HasUpstream = true;
            }

            // Get working directory status
            var statusResult = Execute("git status --porcelain", timeoutMs: 3000);
            if (!statusResult.Contains("Error") && !statusResult.Contains("not a git repository"))
            {
                var lines = statusResult.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    if (line.Length >= 2)
                    {
                        var staged = line[0];
                        var unstaged = line[1];

                        // Count conflicts (both staged and unstaged show as modified)
                        if (line.StartsWith("UU") || line.StartsWith("AA") || line.StartsWith("DD"))
                            info.ConflictCount++;
                        else if (staged != ' ' && staged != '?') info.StagedCount++;

                        if (unstaged != ' ' && unstaged != '?' && !line.StartsWith("UU") && !line.StartsWith("AA") && !line.StartsWith("DD"))
                            info.UnstagedCount++;

                        if (line.StartsWith("??")) info.UntrackedCount++;
                    }
                }
                info.HasUncommittedChanges = info.StagedCount > 0 || info.UnstagedCount > 0 || info.UntrackedCount > 0 || info.ConflictCount > 0;
            }

            // Get ahead/behind status
            if (info.HasUpstream)
            {
                var aheadBehindResult = ExecuteFirstLine("git rev-list --count --left-right @{upstream}...HEAD 2>nul", timeoutMs: 2000);
                if (!aheadBehindResult.Contains("Error") && !aheadBehindResult.Contains("No output"))
                {
                    var parts = aheadBehindResult.Split('\t');
                    if (parts.Length >= 2)
                    {
                        if (int.TryParse(parts[0], out var behind))
                            info.BehindCount = behind;
                        if (int.TryParse(parts[1], out var ahead))
                            info.AheadCount = ahead;
                    }
                }
            }

            // Get stash count
            var stashResult = Execute("git stash list", timeoutMs: 2000);
            if (!stashResult.Contains("Error"))
            {
                info.StashCount = stashResult.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length;
            }

            // Check repository state
            var gitDir = ExecuteFirstLine("git rev-parse --git-dir", timeoutMs: 1000);
            if (!gitDir.Contains("Error"))
            {
                try
                {
                    info.IsRebasing = File.Exists(Path.Combine(gitDir, "rebase-merge")) || File.Exists(Path.Combine(gitDir, "rebase-apply"));
                    info.IsMerging = File.Exists(Path.Combine(gitDir, "MERGE_HEAD"));
                }
                catch
                {
                    // Ignore file access errors
                }
            }
        }
        catch
        {
            // Return partially filled info on error
        }

        return info;
    }

    /// <summary>
    /// Get .NET project information from current directory or solution
    /// </summary>
    /// <returns>DotNetProjectInfo with project name/count or null if not found</returns>
    public static DotNetProjectInfo? GetDotNetProjectInfo()
    {
        try
        {
            var currentDir = Directory.GetCurrentDirectory();

            // First: Look for project files in current directory
            var projectFiles = Directory.GetFiles(currentDir, "*.sln")
                .Concat(Directory.GetFiles(currentDir, "*.slnx"))
                .ToArray();

            if (projectFiles.Length > 0)
            {
                if (projectFiles.Length == 1)
                {
                    return new DotNetProjectInfo
                    {
                        ProjectName = Path.GetFileNameWithoutExtension(projectFiles[0]),
                        ProjectCount = 1
                    };
                }
                else
                {
                    return new DotNetProjectInfo
                    {
                        ProjectCount = projectFiles.Length
                    };
                }
            }

            // Second: Try to get from dotnet solution
            if (!IsCommandAvailable("dotnet"))
                return null;

            var result = Execute("dotnet sln list 2>nul", timeoutMs: 2000);
            if (result.Contains("Error") || result.Contains("No output"))
                return null;

            var lines = result.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var projectLines = lines.Where(l => l.EndsWith(".csproj") || l.EndsWith(".fsproj") || l.EndsWith(".vbproj")).ToArray();
            
            if (projectLines.Length > 0)
            {
                if (projectLines.Length == 1)
                {
                    return new DotNetProjectInfo
                    {
                        ProjectName = Path.GetFileNameWithoutExtension(Path.GetFileName(projectLines[0])),
                        ProjectCount = 1
                    };
                }
                else
                {
                    return new DotNetProjectInfo
                    {
                        ProjectCount = projectLines.Length
                    };
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Get .NET project name from current directory or subdirectories (legacy method)
    /// </summary>
    /// <returns>Project name or null if not found</returns>
    public static string? GetDotNetProjectName()
    {
        var info = GetDotNetProjectInfo();
        return info?.ProjectName;
    }
}

/// <summary>
/// .NET project information
/// </summary>
public class DotNetProjectInfo
{
    /// <summary>
    /// Project name (only set when ProjectCount == 1)
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// Total number of projects found
    /// </summary>
    public int ProjectCount { get; set; }
}

/// <summary>
/// Git status information
/// </summary>
public class GitStatusInfo
{
    // Branch information
    public string? BranchName { get; set; }
    public bool IsDetachedHead { get; set; }
    public string? UpstreamBranch { get; set; }
    public bool HasUpstream { get; set; }

    // Commit counts
    public int AheadCount { get; set; }
    public int BehindCount { get; set; }

    // File change counts  
    public int StagedCount { get; set; }
    public int UnstagedCount { get; set; }
    public int UntrackedCount { get; set; }
    public int ConflictCount { get; set; }
    public int StashCount { get; set; }

    // Repository state
    public bool HasUncommittedChanges { get; set; }
    public bool IsRebasing { get; set; }
    public bool IsMerging { get; set; }

    // Legacy properties for backward compatibility
    public int Unstaged => UnstagedCount;
    public int Staged => StagedCount;
    public int Untracked => UntrackedCount;
    public int Ahead => AheadCount;
    public int Behind => BehindCount;

    public bool HasChanges => StagedCount > 0 || UnstagedCount > 0 || UntrackedCount > 0 || ConflictCount > 0;
    public bool HasRemoteChanges => AheadCount > 0 || BehindCount > 0;

    public override string ToString()
    {
        var parts = new List<string>();

        if (StagedCount > 0) parts.Add($"+{StagedCount}");
        if (UnstagedCount > 0) parts.Add($"~{UnstagedCount}");
        if (UntrackedCount > 0) parts.Add($"?{UntrackedCount}");
        if (ConflictCount > 0) parts.Add($"!{ConflictCount}");
        if (AheadCount > 0) parts.Add($"↑{AheadCount}");
        if (BehindCount > 0) parts.Add($"↓{BehindCount}");
        if (StashCount > 0) parts.Add($"$${StashCount}");

        return parts.Count > 0 ? string.Join(" ", parts) : "clean";
    }
}