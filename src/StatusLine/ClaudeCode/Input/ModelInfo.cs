using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Input;

/// <summary>
/// Information about the AI model being used in the current session
/// </summary>
public class ModelInfo
{
    /// <summary>
    /// Unique identifier for the model (e.g., "claude-3-5-sonnet-20241022")
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Human-readable name of the model (e.g., "Claude 3.5 Sonnet")
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;
}