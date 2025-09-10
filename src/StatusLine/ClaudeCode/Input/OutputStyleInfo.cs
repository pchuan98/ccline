using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Input;

/// <summary>
/// Output style configuration for the status line display
/// </summary>
public class OutputStyleInfo
{
    /// <summary>
    /// Name of the output style (e.g., "default", "compact", "detailed")
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}