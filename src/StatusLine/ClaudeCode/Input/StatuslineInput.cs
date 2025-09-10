using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using StatusLine.ClaudeCode.Input;

namespace StatusLine.ClaudeCode;

/// <summary>
/// JSON serialization context for StatuslineInput with snake_case property naming
/// </summary>
[JsonSerializable(typeof(StatuslineInput))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower,
    WriteIndented = true,
    GenerationMode = JsonSourceGenerationMode.Default)]
public partial class StatuslineInputContext : JsonSerializerContext;

/// <summary>
/// Input data passed to statusline plugins from Claude Code
/// </summary>
public class StatuslineInput
{
    /// <summary>
    /// Name of the hook event that triggered this input
    /// </summary>
    [JsonPropertyName("hook_event_name")]
    public string HookEventName { get; set; } = string.Empty;

    /// <summary>
    /// Unique identifier for the current session
    /// </summary>
    [JsonPropertyName("session_id")]
    public string SessionId { get; set; } = string.Empty;

    /// <summary>
    /// File path to the session transcript
    /// </summary>
    [JsonPropertyName("transcript_path")]
    public string TranscriptPath { get; set; } = string.Empty;

    /// <summary>
    /// Current working directory
    /// </summary>
    [JsonPropertyName("cwd")]
    public string Cwd { get; set; } = string.Empty;

    /// <summary>
    /// Information about the AI model being used
    /// </summary>
    [JsonPropertyName("model")]
    public ModelInfo Model { get; set; } = new();

    /// <summary>
    /// Information about the current workspace
    /// </summary>
    [JsonPropertyName("workspace")]
    public WorkspaceInfo Workspace { get; set; } = new();

    /// <summary>
    /// Version of Claude Code
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Output style configuration for the status line
    /// </summary>
    [JsonPropertyName("output_style")]
    public OutputStyleInfo OutputStyle { get; set; } = new();

    /// <summary>
    /// Cost information for the current session
    /// </summary>
    [JsonPropertyName("cost")]
    public CostInfo Cost { get; set; } = new();

    /// <summary>
    /// Deserializes StatuslineInput from JSON string
    /// </summary>
    /// <param name="json">JSON string to deserialize</param>
    /// <returns>StatuslineInput instance or null if deserialization fails</returns>
    public static StatuslineInput? FromJson(string json)
    {
        try
        {
            return JsonSerializer.Deserialize(json, StatuslineInputContext.Default.StatuslineInput);
        }
        catch (JsonException e)
        {
            var header = $"[StatuslineInput] {e.Message}";
            var jsonStr = json;

            Console.WriteLine($"{header}\n{jsonStr}");
        }
        return null;
    }

    /// <summary>
    /// Preprocesses JSON string to properly escape Windows file paths
    /// </summary>
    /// <param name="json">Original JSON string</param>
    /// <returns>JSON string with properly escaped paths</returns>
    private static string PreprocessJsonPaths(string json)
    {
        // Pattern to match path values in JSON (paths typically start with drive letter or UNC)
        var pathPattern = @"""([A-Za-z]:\\[^""]*|\\\\[^""]*|[^""]*\.js[^""]*|[^""]*\\.claude[^""]*)""]";

        return Regex.Replace(json, pathPattern, match =>
        {
            var path = match.Groups[1].Value;
            // Escape backslashes in the path
            var escapedPath = path.Replace("\\", "\\\\");
            return $"\"{escapedPath}\"";
        });
    }

    /// <summary>
    /// Serializes StatuslineInput to JSON string
    /// </summary>
    /// <returns>JSON string representation of the input</returns>
    public string ToJson()
        => JsonSerializer.Serialize(this, StatuslineInputContext.Default.StatuslineInput);
}