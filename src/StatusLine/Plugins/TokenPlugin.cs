using System.Drawing;
using StatusLine.ClaudeCode;
using StatusLine.Components;
using StatusLine.Models;

namespace StatusLine.Plugins;

/// <summary>
/// Plugin that displays token usage information in the status line
/// Supports both simple token count and progress bar with configurable limits
/// </summary>
public class TokenPlugin : IPlugin
{
    /// <summary>
    /// Current input context (set by DI container or manually)
    /// </summary>
    public StatuslineInput? Input { get; set; }

    /// <summary>
    /// Configuration for token display and progress bar
    /// </summary>
    public TokenPluginConfig Config { get; set; } = new();

    public int Tokens { get; private set; }

    /// <summary>
    /// Rendered string output for the status line
    /// </summary>
    public TextModel[] TextArray
    {
        get
        {
            if (!Config.IsEnabled) return [];

            var currentTokens = SessionUtil.Tokens;

            if (currentTokens < 0) return [];

            var percentage = Math.Min(1, (double)currentTokens / Config.MaxTokens);

            var processbar = new ProgressBar(ProgressBarThemes.Green2Red, Config.Opacity)
            {
                Value = percentage,
                EmptyChar = ProgressBar.EmptyBlock,
                FilledChar = ProgressBar.FillBlock,
                FillColor = Color.LightSeaGreen,
                EmptyColor = Color.Gray,
                Width = Config.Width
            }.TextArray;

            var left = new TextModel("[")
            {
                Opacity = Config.Opacity,
                Foreground = ProgressBarThemes.Green2Red.Invoke(0)
            };
            var right = new TextModel("]")
            {
                Opacity = Config.Opacity,
                Foreground = ProgressBarThemes.Green2Red.Invoke(percentage)
            };

            var tk = new TextModel($" {currentTokens}") { Opacity = Config.Opacity * 0.5 };

            return [ .. processbar, tk];
        }
    }
}

/// <summary>
/// Configuration for TokenPlugin behavior
/// </summary>
public class TokenPluginConfig
{
    /// <summary>
    /// Whether the plugin is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// Plugin name for identification
    /// </summary>
    public string Name { get; set; } = "Token";

    /// <summary>
    /// Maximum token limit for progress bar (0 = simple count display)
    /// </summary>
    public int MaxTokens { get; set; } = 150000;

    /// <summary>
    /// Width of the progress bar
    /// </summary>
    public int Width { get; set; } = 20;

    /// <summary>
    /// Opacity for RGB colors (0.0 to 1.0)
    /// </summary>
    public double Opacity { get; set; } = 0.7;
}