using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// Represents a todo item in Claude Code's task management system
/// </summary>
public class TodoItem
{
    /// <summary>
    /// Description of the todo item
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Current status of the todo item (e.g., "pending", "completed")
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Active form of the todo item for display purposes
    /// </summary>
    [JsonPropertyName("activeForm")]
    public string ActiveForm { get; set; } = string.Empty;
}