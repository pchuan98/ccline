using System.Text.Json;
using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// JSON serialization context for SessionMessage with camelCase property naming
/// </summary>
[JsonSerializable(typeof(SessionMessage))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    GenerationMode = JsonSourceGenerationMode.Default)]
public partial class SessionMessageContext : JsonSerializerContext;

/// <summary>
/// Represents a session message in Claude Code
/// </summary>
public class SessionMessage
{
    /// <summary>
    /// UUID of the parent message in the conversation thread
    /// </summary>
    [JsonPropertyName("parentUuid")]
    public string? ParentUuid { get; set; }

    /// <summary>
    /// Whether this message is part of a sidechain conversation
    /// </summary>
    [JsonPropertyName("isSidechain")]
    public bool IsSidechain { get; set; }

    /// <summary>
    /// Type of user (e.g., "human", "assistant")
    /// </summary>
    [JsonPropertyName("userType")]
    public string UserType { get; set; } = string.Empty;

    /// <summary>
    /// Current working directory when the message was created
    /// </summary>
    [JsonPropertyName("cwd")]
    public string Cwd { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier for the session
    /// </summary>
    [JsonPropertyName("sessionId")]
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// Version of Claude Code that created this message
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Git branch name when the message was created
    /// </summary>
    [JsonPropertyName("gitBranch")]
    public string GitBranch { get; set; } = string.Empty;

    /// <summary>
    /// Type of message (e.g., "message", "tool_use")
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Content of the message including usage statistics
    /// </summary>
    [JsonPropertyName("message")]
    public MessageContent Message { get; set; } = new();

    /// <summary>
    /// Unique identifier for this message
    /// </summary>
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the message was created
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Request identifier for tracking API calls
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; set; }

    /// <summary>
    /// Result of tool use operations in this message
    /// </summary>
    [JsonPropertyName("toolUseResult")]
    public object? ToolUseResult { get; set; }

    /// <summary>
    /// Deserializes SessionMessage from JSON string
    /// </summary>
    /// <param name="json">JSON string to deserialize</param>
    /// <returns>SessionMessage instance or null if deserialization fails</returns>
    public static SessionMessage? FromJson(string json) => JsonSerializer.Deserialize(json, SessionMessageContext.Default.SessionMessage);

    /// <summary>
    /// Serializes SessionMessage to JSON string
    /// </summary>
    /// <returns>JSON string representation of the message</returns>
    public string ToJson() => JsonSerializer.Serialize(this, SessionMessageContext.Default.SessionMessage);
}