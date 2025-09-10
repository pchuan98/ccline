using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// Represents the content of a Claude Code message including usage statistics
/// </summary>
public class MessageContent
{
    /// <summary>
    /// Unique identifier for the message
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Type of message content
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Role of the message sender (e.g., "user", "assistant")
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Model used to generate the response
    /// </summary>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// The actual content of the message
    /// </summary>
    [JsonPropertyName("content")]
    public object Content { get; set; } = new();

    /// <summary>
    /// Reason why the message generation stopped
    /// </summary>
    [JsonPropertyName("stop_reason")]
    public string? StopReason { get; set; }

    /// <summary>
    /// Sequence that triggered the stop
    /// </summary>
    [JsonPropertyName("stop_sequence")]
    public string? StopSequence { get; set; }

    /// <summary>
    /// Token usage statistics for this message
    /// </summary>
    [JsonPropertyName("usage")]
    public UsageStatistics? Usage { get; set; }

    /// <summary>
    /// Request identifier for tracking API calls
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }
}