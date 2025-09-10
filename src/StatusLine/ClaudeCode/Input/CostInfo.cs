using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Input;

/// <summary>
/// Cost and usage statistics for the current session
/// </summary>
public class CostInfo
{
    /// <summary>
    /// Total cost in USD for the current session
    /// </summary>
    [JsonPropertyName("total_cost_usd")]
    public decimal TotalCostUsd { get; set; }

    /// <summary>
    /// Total duration of the session in milliseconds
    /// </summary>
    [JsonPropertyName("total_duration_ms")]
    public int TotalDurationMs { get; set; }

    /// <summary>
    /// Total API call duration in milliseconds
    /// </summary>
    [JsonPropertyName("total_api_duration_ms")]
    public int TotalApiDurationMs { get; set; }

    /// <summary>
    /// Total number of lines added to files during the session
    /// </summary>
    [JsonPropertyName("total_lines_added")]
    public int TotalLinesAdded { get; set; }

    /// <summary>
    /// Total number of lines removed from files during the session
    /// </summary>
    [JsonPropertyName("total_lines_removed")]
    public int TotalLinesRemoved { get; set; }
}