using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Session;

/// <summary>
/// Represents token usage statistics for a Claude Code session
/// </summary>
public class UsageStatistics
{
    /// <summary>
    /// Number of input tokens used in the request
    /// </summary>
    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    /// <summary>
    /// Number of input tokens used for cache creation
    /// </summary>
    [JsonPropertyName("cache_creation_input_tokens")]
    public int CacheCreationInputTokens { get; set; }

    /// <summary>
    /// Number of input tokens read from cache
    /// </summary>
    [JsonPropertyName("cache_read_input_tokens")]
    public int CacheReadInputTokens { get; set; }

    /// <summary>
    /// Cache creation details if applicable
    /// </summary>
    [JsonPropertyName("cache_creation")]
    public CacheCreation? CacheCreation { get; set; }

    /// <summary>
    /// Number of output tokens generated in the response
    /// </summary>
    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }

    /// <summary>
    /// Service tier used for processing the request
    /// </summary>
    [JsonPropertyName("service_tier")]
    public string ServiceTier { get; set; } = string.Empty;

    /// <summary>
    /// Returns a tab-separated string representation of token usage
    /// </summary>
    /// <returns>Tab-separated string with token counts</returns>
    public override string ToString()
        => $"{InputTokens}\t" +
           $"{CacheCreationInputTokens}\t" +
           $"{CacheReadInputTokens}\t" +
           $"{OutputTokens}";

    /// <summary>
    /// Gets the total number of tokens used (input + output + cache)
    /// </summary>
    public int Tokens => InputTokens + CacheCreationInputTokens + CacheReadInputTokens + OutputTokens;
}