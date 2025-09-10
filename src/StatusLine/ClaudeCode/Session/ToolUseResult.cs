using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// Represents the result of a tool use operation in Claude Code
/// </summary>
public class ToolUseResult
{
    /// <summary>
    /// List of filenames affected by the tool operation
    /// </summary>
    [JsonPropertyName("filenames")]
    public List<string>? Filenames { get; set; }

    /// <summary>
    /// Duration of the tool operation in milliseconds
    /// </summary>
    [JsonPropertyName("durationMs")]
    public int? DurationMs { get; set; }

    [JsonPropertyName("numFiles")]
    public int? NumFiles { get; set; }

    [JsonPropertyName("truncated")]
    public bool? Truncated { get; set; }

    [JsonPropertyName("oldTodos")]
    public List<object>? OldTodos { get; set; }

    /// <summary>
    /// New todo items created by the tool operation
    /// </summary>
    [JsonPropertyName("newTodos")]
    public List<TodoItem>? NewTodos { get; set; }

    [JsonPropertyName("query")]
    public string? Query { get; set; }

    [JsonPropertyName("results")]
    public List<object>? Results { get; set; }

    [JsonPropertyName("durationSeconds")]
    public decimal? DurationSeconds { get; set; }

    [JsonPropertyName("filePath")]
    public string? FilePath { get; set; }

    [JsonPropertyName("oldString")]
    public string? OldString { get; set; }

    [JsonPropertyName("newString")]
    public string? NewString { get; set; }

    [JsonPropertyName("originalFile")]
    public string? OriginalFile { get; set; }

    [JsonPropertyName("structuredPatch")]
    public object? StructuredPatch { get; set; }

    [JsonPropertyName("userModified")]
    public bool? UserModified { get; set; }

    [JsonPropertyName("replaceAll")]
    public bool? ReplaceAll { get; set; }

    /// <summary>
    /// Standard output from the tool operation
    /// </summary>
    [JsonPropertyName("stdout")]
    public string? Stdout { get; set; }

    /// <summary>
    /// Standard error from the tool operation
    /// </summary>
    [JsonPropertyName("stderr")]
    public string? Stderr { get; set; }

    [JsonPropertyName("interrupted")]
    public bool? Interrupted { get; set; }

    [JsonPropertyName("isImage")]
    public bool? IsImage { get; set; }

    /// <summary>
    /// Type of tool operation performed
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
}