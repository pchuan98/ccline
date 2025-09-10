using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Config;

/// <summary>
/// Represents a cached access permission item for feature gates
/// </summary>
public class AccessCacheItem
{
    /// <summary>
    /// Whether the user has access to the feature
    /// </summary>
    [JsonPropertyName("hasAccess")]
    public bool HasAccess { get; set; }

    /// <summary>
    /// Unix timestamp when this cache entry was created
    /// </summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }
}