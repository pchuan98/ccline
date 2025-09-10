using StatusLine.ClaudeCode;
using StatusLine.ClaudeCode.Session;

namespace StatusLine;

/// <summary>
/// Utility class for session operations with caching support
/// </summary>
public static class SessionUtil
{
    /// <summary>
    /// Cache of all session messages across all transcript files
    /// </summary>
    public static Dictionary<string, SessionMessage[]> Sessions { get; private set; } = new();

    /// <summary>
    /// Current input data from Claude Code
    /// </summary>
    public static StatuslineInput? CurrentInput { get; private set; }

    /// <summary>
    /// Current active session message
    /// </summary>
    public static SessionMessage?[]? CurrentSessions { get; private set; }

    /// <summary>
    /// Updates session data from JSON input and loads all available sessions
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when transcript file doesn't exist</exception>
    public static void Update(StatuslineInput? input)
    {
        if (CurrentInput is not null) return;

        if (!File.Exists(input?.TranscriptPath))
            throw new ArgumentException($"File \"{input?.TranscriptPath}\" does not exist.");

        CurrentInput = input;

        CurrentSessions = File.ReadAllLines(input.TranscriptPath)
            .Select(SessionMessage.FromJson)
            .ToArray();

        //Sessions.Add();
    }

    /// <summary>
    /// Calculates total tokens used across all sessions
    /// </summary>
    /// <returns>Total number of tokens used</returns>
    public static int Tokens => CurrentSessions
        ?.Select(item => item?.Message.Usage)
        .Where(usage => usage?.Tokens != null)
        .Max(item => item?.Tokens) ?? -1;
}