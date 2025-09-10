using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Config;

/// <summary>
/// Project-specific configuration settings for Claude Code
/// </summary>
public class ProjectConfig
{
    /// <summary>
    /// List of tools that are allowed to be used in this project
    /// </summary>
    [JsonPropertyName("allowedTools")]
    public List<string> AllowedTools { get; set; } = new();

    /// <summary>
    /// Command history for this project
    /// </summary>
    [JsonPropertyName("history")]
    public List<HistoryItem> History { get; set; } = new();

    /// <summary>
    /// MCP (Model Context Protocol) context URIs for this project
    /// </summary>
    [JsonPropertyName("mcpContextUris")]
    public List<string> McpContextUris { get; set; } = new();

    [JsonPropertyName("mcpServers")]
    public Dictionary<string, object> McpServers { get; set; } = new();

    [JsonPropertyName("enabledMcpjsonServers")]
    public List<string> EnabledMcpjsonServers { get; set; } = new();

    [JsonPropertyName("disabledMcpjsonServers")]
    public List<string> DisabledMcpjsonServers { get; set; } = new();

    /// <summary>
    /// Whether the user has accepted the trust dialog for this project
    /// </summary>
    [JsonPropertyName("hasTrustDialogAccepted")]
    public bool HasTrustDialogAccepted { get; set; }

    [JsonPropertyName("projectOnboardingSeenCount")]
    public int ProjectOnboardingSeenCount { get; set; }

    [JsonPropertyName("hasClaudeMdExternalIncludesApproved")]
    public bool HasClaudeMdExternalIncludesApproved { get; set; }

    [JsonPropertyName("hasClaudeMdExternalIncludesWarningShown")]
    public bool HasClaudeMdExternalIncludesWarningShown { get; set; }

    [JsonPropertyName("lastTotalWebSearchRequests")]
    public int LastTotalWebSearchRequests { get; set; }

    /// <summary>
    /// Cost of the last session in this project
    /// </summary>
    [JsonPropertyName("lastCost")]
    public decimal LastCost { get; set; }

    [JsonPropertyName("lastAPIDuration")]
    public int LastAPIDuration { get; set; }

    [JsonPropertyName("lastToolDuration")]
    public int LastToolDuration { get; set; }

    [JsonPropertyName("lastDuration")]
    public int LastDuration { get; set; }

    [JsonPropertyName("lastLinesAdded")]
    public int LastLinesAdded { get; set; }

    [JsonPropertyName("lastLinesRemoved")]
    public int LastLinesRemoved { get; set; }

    [JsonPropertyName("lastTotalInputTokens")]
    public int LastTotalInputTokens { get; set; }

    [JsonPropertyName("lastTotalOutputTokens")]
    public int LastTotalOutputTokens { get; set; }

    [JsonPropertyName("lastTotalCacheCreationInputTokens")]
    public int LastTotalCacheCreationInputTokens { get; set; }

    [JsonPropertyName("lastTotalCacheReadInputTokens")]
    public int LastTotalCacheReadInputTokens { get; set; }

    /// <summary>
    /// ID of the last session in this project
    /// </summary>
    [JsonPropertyName("lastSessionId")]
    public string LastSessionId { get; set; } = string.Empty;
}