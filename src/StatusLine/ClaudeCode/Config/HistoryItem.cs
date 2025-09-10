using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Config;

/// <summary>
/// Represents a history item in the project's command history
/// </summary>
public class HistoryItem
{
    /// <summary>
    /// Display text shown in the history interface
    /// </summary>
    [JsonPropertyName("display")]
    public string Display { get; set; } = string.Empty;

    /// <summary>
    /// Contents that were pasted, keyed by content type
    /// </summary>
    [JsonPropertyName("pastedContents")]
    public Dictionary<string, object> PastedContents { get; set; } = new();
}