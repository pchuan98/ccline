using System.Text.Json.Serialization;

namespace StatusLine.ClaudeCode.Config;

/// <summary>
/// Represents OAuth account information for Claude Code authentication
/// </summary>
public class OAuthAccount
{
    /// <summary>
    /// Unique identifier for the OAuth account
    /// </summary>
    [JsonPropertyName("accountUuid")]
    public string AccountUuid { get; set; } = string.Empty;

    /// <summary>
    /// Email address associated with the OAuth account
    /// </summary>
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier for the organization
    /// </summary>
    [JsonPropertyName("organizationUuid")]
    public string OrganizationUuid { get; set; } = string.Empty;

    /// <summary>
    /// Role of the user within the organization
    /// </summary>
    [JsonPropertyName("organizationRole")]
    public string OrganizationRole { get; set; } = string.Empty;

    /// <summary>
    /// Role of the user within the workspace (if applicable)
    /// </summary>
    [JsonPropertyName("workspaceRole")]
    public string? WorkspaceRole { get; set; }

    /// <summary>
    /// Name of the organization
    /// </summary>
    [JsonPropertyName("organizationName")]
    public string OrganizationName { get; set; } = string.Empty;
}