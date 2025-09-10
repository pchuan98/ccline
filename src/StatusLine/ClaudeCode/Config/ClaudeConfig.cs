using System.Text.Json;
using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Config;

/// <summary>
/// JSON serialization context for ClaudeConfig with camelCase property naming
/// </summary>
[JsonSerializable(typeof(ClaudeConfig))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    GenerationMode = JsonSourceGenerationMode.Default)]
public partial class ClaudeConfigContext : JsonSerializerContext;

/// <summary>
/// Configuration settings for Claude Code application
/// </summary>
public class ClaudeConfig
{
    /// <summary>
    /// Number of times the application has been started
    /// </summary>
    [JsonPropertyName("numStartups")]
    public int NumStartups { get; set; }

    /// <summary>
    /// Method used to install the application
    /// </summary>
    [JsonPropertyName("installMethod")]
    public string InstallMethod { get; set; } = string.Empty;

    /// <summary>
    /// Whether automatic updates are enabled
    /// </summary>
    [JsonPropertyName("autoUpdates")]
    public bool AutoUpdates { get; set; }

    /// <summary>
    /// Current theme setting for the application
    /// </summary>
    [JsonPropertyName("theme")]
    public string Theme { get; set; } = string.Empty;

    /// <summary>
    /// History of tips shown to the user with display counts
    /// </summary>
    [JsonPropertyName("tipsHistory")]
    public Dictionary<string, int> TipsHistory { get; set; } = new();

    /// <summary>
    /// Count of memory usage operations
    /// </summary>
    [JsonPropertyName("memoryUsageCount")]
    public int MemoryUsageCount { get; set; }

    /// <summary>
    /// Count of prompt queue usage operations
    /// </summary>
    [JsonPropertyName("promptQueueUseCount")]
    public int PromptQueueUseCount { get; set; }

    /// <summary>
    /// Cached Statsig feature gate values
    /// </summary>
    [JsonPropertyName("cachedStatsigGates")]
    public Dictionary<string, bool> CachedStatsigGates { get; set; } = new();

    /// <summary>
    /// Timestamp of the first application startup
    /// </summary>
    [JsonPropertyName("firstStartTime")]
    public DateTime FirstStartTime { get; set; }

    /// <summary>
    /// Unique identifier for the current user
    /// </summary>
    [JsonPropertyName("userID")]
    public string UserID { get; set; } = string.Empty;

    /// <summary>
    /// Project-specific configuration settings
    /// </summary>
    [JsonPropertyName("projects")]
    public Dictionary<string, ProjectConfig> Projects { get; set; } = new();

    /// <summary>
    /// Date when Claude Code was first used with tokens
    /// </summary>
    [JsonPropertyName("claudeCodeFirstTokenDate")]
    public DateTime ClaudeCodeFirstTokenDate { get; set; }

    /// <summary>
    /// Whether the user has completed the onboarding process
    /// </summary>
    [JsonPropertyName("hasCompletedOnboarding")]
    public bool HasCompletedOnboarding { get; set; }

    /// <summary>
    /// Version of the last completed onboarding
    /// </summary>
    [JsonPropertyName("lastOnboardingVersion")]
    public string LastOnboardingVersion { get; set; } = string.Empty;

    /// <summary>
    /// Whether the user has Opus plan as default
    /// </summary>
    [JsonPropertyName("hasOpusPlanDefault")]
    public bool HasOpusPlanDefault { get; set; }

    /// <summary>
    /// Cache for S1M access permissions
    /// </summary>
    [JsonPropertyName("s1mAccessCache")]
    public Dictionary<string, AccessCacheItem> S1mAccessCache { get; set; } = new();

    /// <summary>
    /// Whether the user is qualified for data sharing
    /// </summary>
    [JsonPropertyName("isQualifiedForDataSharing")]
    public bool IsQualifiedForDataSharing { get; set; }

    /// <summary>
    /// Warning threshold for fallback availability
    /// </summary>
    [JsonPropertyName("fallbackAvailableWarningThreshold")]
    public decimal FallbackAvailableWarningThreshold { get; set; }

    /// <summary>
    /// Count of subscription notices shown to the user
    /// </summary>
    [JsonPropertyName("subscriptionNoticeCount")]
    public int SubscriptionNoticeCount { get; set; }

    /// <summary>
    /// Whether the user has an available subscription
    /// </summary>
    [JsonPropertyName("hasAvailableSubscription")]
    public bool HasAvailableSubscription { get; set; }

    /// <summary>
    /// Cached changelog content
    /// </summary>
    [JsonPropertyName("cachedChangelog")]
    public string CachedChangelog { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when changelog was last fetched
    /// </summary>
    [JsonPropertyName("changelogLastFetched")]
    public long ChangelogLastFetched { get; set; }

    /// <summary>
    /// Version of the last release notes seen by the user
    /// </summary>
    [JsonPropertyName("lastReleaseNotesSeen")]
    public string LastReleaseNotesSeen { get; set; } = string.Empty;

    /// <summary>
    /// Recommended subscription plan for the user
    /// </summary>
    [JsonPropertyName("recommendedSubscription")]
    public string RecommendedSubscription { get; set; } = string.Empty;

    /// <summary>
    /// Cache for S1M non-subscriber access permissions
    /// </summary>
    [JsonPropertyName("s1mNonSubscriberAccessCache")]
    public Dictionary<string, AccessCacheItem> S1mNonSubscriberAccessCache { get; set; } = new();

    /// <summary>
    /// Whether bypass permissions mode has been accepted
    /// </summary>
    [JsonPropertyName("bypassPermissionsModeAccepted")]
    public bool BypassPermissionsModeAccepted { get; set; }

    /// <summary>
    /// Whether the user has used backslash return functionality
    /// </summary>
    [JsonPropertyName("hasUsedBackslashReturn")]
    public bool HasUsedBackslashReturn { get; set; }

    /// <summary>
    /// OAuth account information for the user
    /// </summary>
    [JsonPropertyName("oauthAccount")]
    public OAuthAccount? OauthAccount { get; set; }

    /// <summary>
    /// Whether the user has acknowledged the cost threshold
    /// </summary>
    [JsonPropertyName("hasAcknowledgedCostThreshold")]
    public bool HasAcknowledgedCostThreshold { get; set; }

    /// <summary>
    /// Deserializes ClaudeConfig from JSON string
    /// </summary>
    /// <param name="json">JSON string to deserialize</param>
    /// <returns>ClaudeConfig instance or null if deserialization fails</returns>
    public static ClaudeConfig? FromJson(string json)
    {
        return JsonSerializer.Deserialize(json, ClaudeConfigContext.Default.ClaudeConfig);
    }

    /// <summary>
    /// Serializes ClaudeConfig to JSON string
    /// </summary>
    /// <returns>JSON string representation of the config</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(this, ClaudeConfigContext.Default.ClaudeConfig);
    }
}