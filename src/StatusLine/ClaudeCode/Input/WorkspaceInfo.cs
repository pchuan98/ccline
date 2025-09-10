using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Input;

/// <summary>
/// Information about the current workspace and project directories
/// </summary>
public class WorkspaceInfo
{
    /// <summary>
    /// Current working directory path
    /// </summary>
    [JsonPropertyName("current_dir")]
    public string CurrentDir { get; set; } = string.Empty;

    /// <summary>
    /// Project root directory path
    /// </summary>
    [JsonPropertyName("project_dir")]
    public string ProjectDir { get; set; } = string.Empty;
}