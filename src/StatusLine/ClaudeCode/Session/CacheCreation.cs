using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// Represents cache creation statistics for token usage
/// </summary>
public class CacheCreation
{
    /// <summary>
    /// Number of input tokens used for 5-minute ephemeral cache creation
    /// </summary>
    [JsonPropertyName("ephemeral_5m_input_tokens")]
    public int Ephemeral5mInputTokens { get; set; }

    /// <summary>
    /// Number of input tokens used for 1-hour ephemeral cache creation
    /// </summary>
    [JsonPropertyName("ephemeral_1h_input_tokens")]
    public int Ephemeral1hInputTokens { get; set; }
}